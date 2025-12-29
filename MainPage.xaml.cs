using System.Collections.ObjectModel;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public ObservableCollection<Item> ListaItems { get; set; }

	public MainPage()
	{
		InitializeComponent();

		ListaItems = new ObservableCollection<Item>
		{
			new Item { Nombre = "Santander", Deuda = "500.00", Corte = "12/12/2025", UltPag = "25/12/2025" },
		};

		BindingContext = this;

	}

	private void OnAgregarClicked(object sender, EventArgs e)
	{
		ListaItems.Add(new Item
		{
			Nombre = txtBanco.Text,
			Deuda = txtPago.Text,
			Corte = txtCorte.Text,
			UltPag = txtUltPag.Text
		});

		txtBanco.Text = "";
		txtPago.Text = "";
		txtCorte.Text = "";
		txtUltPag.Text = "";
	}

	private void OnEliminarClicked(object sender, EventArgs e)
	{
		var boton = sender as Button;
		var item = boton?.CommandParameter as Item;

		if (item != null)
		{
			ListaItems.Remove(item);
		}
	}
}

public class Item
{
	public string? Nombre { get; set; }
	public string? Deuda { get; set; }
	public string? Corte { get; set; }
	public string? UltPag { get; set; }
}

