using Java.Net;
using Newtonsoft.Json;
using proyecto_jgarrido.Models;
using System.Collections.ObjectModel;

namespace proyecto_jgarrido.Views;

public partial class Comments : ContentPage
{
    private const string Url = "http://10.2.3.102:9000/comments/activity/";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Models.ComentarioModel> comments;
    private Actividad _selectedActivity;
    public Comments(Actividad selectedActivity)
	{
        InitializeComponent();
        _selectedActivity = selectedActivity;
        LoadCategories();
    }


    private async void LoadCategories()
    {
        var content = await cliente.GetStringAsync(Url + _selectedActivity.Id);
        List<Models.ComentarioModel> mostrarCom = JsonConvert.DeserializeObject<List<Models.ComentarioModel>>(content);
        comments = new ObservableCollection<ComentarioModel>(mostrarCom);
        CommentsListView.ItemsSource = comments;
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

    private async void Button_Comentary(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new NewComment(_selectedActivity));
    }
}
