using Newtonsoft.Json;
using proyecto_jgarrido.Models;
using System.Collections.ObjectModel;

namespace proyecto_jgarrido.Views;

public partial class Categories : ContentPage
{
    private const string Url = "http://10.204.14.206:9000/categories";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Models.Categoria> category;
    public Categories()
	{
		InitializeComponent();
        LoadCategories();

    }

    private async void LoadCategories()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Models.Categoria> mostrarCat = JsonConvert.DeserializeObject<List<Models.Categoria>>(content);
        category = new ObservableCollection<Categoria>(mostrarCat);
        CategoriesListView.ItemsSource = category;
    }


    private async void OnMenuClicked(object sender, EventArgs e)
    {
        var action = await DisplayActionSheet("Menu", "Cancel", null, "Historial", "Categorias", "Actividades");

        switch (action)
        {
            case "Historial":
                await Navigation.PushAsync(new History());
                break;
            case "Categorias":
                await Navigation.PushAsync(new Categories());
                break;
            case "Actividades":
                await Navigation.PushAsync(new Activities());
                break;
        }
    }

    private async void Button_NewCategory(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new NewCategory());
    }
}
