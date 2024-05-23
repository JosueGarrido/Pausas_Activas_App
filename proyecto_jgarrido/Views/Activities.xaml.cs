using Java.Net;
using Newtonsoft.Json;
using proyecto_jgarrido.Models;
using System.Collections.ObjectModel;

namespace proyecto_jgarrido.Views;

public partial class Activities : ContentPage
{
    
    private const string Url = "http://10.204.14.206:9000/activities";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Models.Actividad> activity;
    public Activities()
    {
        InitializeComponent();
        LoadActivities();
    }

    private async void LoadActivities()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Models.Actividad> mostrarAct = JsonConvert.DeserializeObject<List<Models.Actividad>>(content);
        activity = new ObservableCollection<Actividad>(mostrarAct);
        ActivitiesListView.ItemsSource = activity;
    }

    private async void OnActivitySelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objetoactividad = (Models.Actividad)e.SelectedItem;
        await Navigation.PushAsync(new ActivityDetailPage(objetoactividad));
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

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewActivity());
    }

}