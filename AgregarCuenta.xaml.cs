using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.RegularExpressions;

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
        if(string.IsNullOrWhiteSpace(txtBanco.Text) ||
            string.IsNullOrWhiteSpace(txtPago.Text) ||
            string.IsNullOrWhiteSpace(txtCorte.Text) ||
            string.IsNullOrWhiteSpace(txtUltPag.Text))
        {
            await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            return;
        }

        string json = Preferences.Get("cuentasJSON", "");

        ListaItems = string.IsNullOrEmpty(json)
            ? new ObservableCollection<Item>()
            : JsonSerializer.Deserialize<ObservableCollection<Item>>(json);

        ListaItems.Add(new Item
        {
            Nombre = txtBanco.Text.Trim(),
            Deuda = txtPago.Text.Trim(),
            Corte = txtCorte.Text.Trim(),
            UltPag = txtUltPag.Text.Trim(),
            numeroTarjeta = string.IsNullOrWhiteSpace(txtNumero.Text) ? "" : Regex.Replace(txtNumero.Text.Trim(), ".{4}", "$0 ")
        });

        Preferences.Set("cuentasJSON", JsonSerializer.Serialize(ListaItems));

        txtBanco.Text = "";
        txtPago.Text = "";
        txtCorte.Text = "";
        txtUltPag.Text = "";
        txtNumero.Text = "";

        await Navigation.PopAsync();
    }
}

public class Item
{
    public string? Nombre { get; set; }
    public string? Deuda { get; set; }
    public string? Corte { get; set; }
    public string? UltPag { get; set; }
    public string? numeroTarjeta { get; set; }
}