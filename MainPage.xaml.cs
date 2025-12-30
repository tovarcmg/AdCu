using System.Collections.ObjectModel;
using System.Text.Json;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Item> ListaItems { get; set; }

    public MainPage()
    {
        InitializeComponent();

        string json = Preferences.Get("cuentasJSON", "");

        ListaItems = new ObservableCollection<Item>();

        BindingContext = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        string json = Preferences.Get("cuentasJSON", "");

        ListaItems.Clear();

        if (!string.IsNullOrEmpty(json))
        {
            var items = JsonSerializer.Deserialize<ObservableCollection<Item>>(json);

            double total = 0;

            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.numeroTarjeta) && item.numeroTarjeta.Length == 20)
                    item.numeroTarjeta = "**** **** **** " + item.numeroTarjeta.Substring(15, 4);

                try
                {
                    item.Deuda = "$" + Convert.ToDouble(item.Deuda).ToString("N2");

                    total = total + Convert.ToDouble(item.Deuda);
                }
                catch (Exception)
                {

                }

                ListaItems.Add(item);
            }

            lblCuentas.Text = $"Cuentas ({ListaItems.Count}) - Total: ${total:N2}";

            await tarjetas.ScaleTo(1.09, 200);
            await tarjetas.ScaleTo(1, 200);
        }
    }

    private void OnEliminarClicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        var item = boton?.CommandParameter as Item;

        if (item != null)
        {
            ListaItems.Remove(item);
            Preferences.Set("cuentasJSON", JsonSerializer.Serialize(ListaItems));

            double total = 0;

            foreach (var x in ListaItems)
            {
                try
                {
                    total = total + Convert.ToDouble(x.Deuda);
                }
                catch (Exception)
                {

                }
            }

            lblCuentas.Text = $"Cuentas ({ListaItems.Count}) - Total: ${total:N2}";
        }
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AgregarCuenta());
    }

    private void OnModificarClicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        var item = boton?.CommandParameter as Item;

        if (item != null)
        {
            Navigation.PushAsync(new ModificarCuenta(item.Nombre));
        }
    }
}