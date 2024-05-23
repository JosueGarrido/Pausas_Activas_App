using System.Collections.ObjectModel;
using GoogleGson;
using Newtonsoft.Json;
using proyecto_jgarrido.Models;
using proyecto_jgarrido.Services;

namespace proyecto_jgarrido.Views;

public partial class History : ContentPage
{
    private const string Url = "http://10.2.3.102:9000/users/activity/";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Models.HistoryModel> history;

    public History()
	{
		InitializeComponent();
        LoadHistory();

    }

    private async void LoadHistory()
    {
        var authenticatedUser = UserSession.Instance.AuthenticatedUser;
        if (authenticatedUser == null)
        {
            await DisplayAlert("Error", "No se ha identificado al usuario.", "OK");
            return;
        }

        var content = await cliente.GetStringAsync(Url + authenticatedUser.Id);
        List <Models.HistoryModel> mostrarHis = JsonConvert.DeserializeObject<List<Models.HistoryModel>>(content);
        history = new ObservableCollection<HistoryModel>(mostrarHis);
        HistoryListView.ItemsSource = history;
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
}
