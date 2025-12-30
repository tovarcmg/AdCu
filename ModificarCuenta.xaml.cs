using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MyMauiApp;

public partial class ModificarCuenta : ContentPage
{
    string idBorrar;

    public ModificarCuenta(string id)
    {
        InitializeComponent();

        idBorrar = id;

        string json = Preferences.Get("cuentasJSON", "");

        if (!string.IsNullOrEmpty(json))
        {
            var items = JsonSerializer.Deserialize<ObservableCollection<Item>>(json);

            foreach (var item in items)
            {
                if (item.Nombre == id)
                {
                    txtBanco.Text = item.Nombre;
                    txtPago.Text = item.Deuda;
                    txtCorte.Text = item.Corte;
                    txtUltPag.Text = item.UltPag;
                    txtNumero.Text = item.numeroTarjeta.Replace(" ","");
                }
            }
        }
    }

    private async void btnModificar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBanco.Text) ||
            string.IsNullOrWhiteSpace(txtPago.Text) ||
            string.IsNullOrWhiteSpace(txtCorte.Text) ||
            string.IsNullOrWhiteSpace(txtUltPag.Text))
        {
            await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            return;
        }

        // 1️ Obtener JSON
        string json = Preferences.Get("cuentasJSON", string.Empty);

        // 2️ Deserializar de forma segura
        var items = string.IsNullOrWhiteSpace(json)
            ? new ObservableCollection<Item>()
            : JsonSerializer.Deserialize<ObservableCollection<Item>>(json)
              ?? new ObservableCollection<Item>();

        // 3️ Eliminar si existe
        var itemBorrar = items.FirstOrDefault(x => x.Nombre == idBorrar);
        if (itemBorrar != null)
            items.Remove(itemBorrar);

        // 4️ Agregar nuevo registro
        items.Add(new Item
        {
            Nombre = txtBanco.Text.Trim(),
            Deuda = txtPago.Text.Trim(),
            Corte = txtCorte.Text.Trim(),
            UltPag = txtUltPag.Text.Trim(),
            numeroTarjeta = string.IsNullOrWhiteSpace(txtNumero.Text) ? "" : Regex.Replace(txtNumero.Text.Trim(), ".{4}", "$0 ") 
        });

        // 5️ Guardar nuevamente
        Preferences.Set("cuentasJSON", JsonSerializer.Serialize(items));

        // 6️ Limpiar controles
        txtBanco.Text = string.Empty;
        txtPago.Text = string.Empty;
        txtCorte.Text = string.Empty;
        txtUltPag.Text = string.Empty;
        txtNumero.Text = string.Empty;

        // 7️ Regresar
        await Navigation.PopAsync();
    }
}