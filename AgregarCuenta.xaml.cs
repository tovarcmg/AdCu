using System.Collections.ObjectModel;
using System.Text.Json;

namespace MyMauiApp;

public partial class AgregarCuenta : ContentPage
{
    public ObservableCollection<Item> ListaItems { get; set; }

    public AgregarCuenta()
    {
        InitializeComponent();
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        string json = Preferences.Get("cuentasJSON", "");

        ListaItems = string.IsNullOrEmpty(json)
            ? new ObservableCollection<Item>()
            : JsonSerializer.Deserialize<ObservableCollection<Item>>(json);

        ListaItems.Add(new Item
        {
            Nombre = txtBanco.Text,
            Deuda = txtPago.Text,
            Corte = txtCorte.Text,
            UltPag = txtUltPag.Text
        });

        Preferences.Set("cuentasJSON", JsonSerializer.Serialize(ListaItems));

        txtBanco.Text = "";
        txtPago.Text = "";
        txtCorte.Text = "";
        txtUltPag.Text = "";

        await Navigation.PopAsync();
    }
}

public class Item
{
    public string? Nombre { get; set; }
    public string? Deuda { get; set; }
    public string? Corte { get; set; }
    public string? UltPag { get; set; }
}