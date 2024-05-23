using System.Net;
using System.Text;
using System.Text.Json;
using Android.Net;
using proyecto_jgarrido.Models;
using proyecto_jgarrido.Services;

namespace proyecto_jgarrido.Views;

public partial class ActivityDetailPage : ContentPage
{
    private readonly HttpClient httpClient;
    private Actividad _selectedActivity;

    public ActivityDetailPage(Actividad selectedActivity)
	{
		InitializeComponent();
        _selectedActivity = selectedActivity;
        BindActivityDetails();
        httpClient = new HttpClient();
        
    }

    public Actividad Objetoactividad { get; }

    private void BindActivityDetails()
    {
        ActivityNameLabel.Text = _selectedActivity.Nombre;
        ActivityDescriptionLabel.Text = _selectedActivity.Descripcion;
        ActivityDurationLabel.Text = $"Duración: {_selectedActivity.Duracion} minutos";
    }

    private async void OnRegisterActivityClicked(object sender, EventArgs e)
    {
        var authenticatedUser = UserSession.Instance.AuthenticatedUser;
        if (authenticatedUser == null)
        {
            await DisplayAlert("Error", "No se ha identificado al usuario.", "OK");
            return;
        }

        try
        {
            var registroActividad = new NewHistory
            {
                Usuario_Id = authenticatedUser.Id,
                Actividad_Id = _selectedActivity.Id
            };

            string json = JsonSerializer.Serialize(registroActividad);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await httpClient.PostAsync("http://10.2.3.102:9000/history", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Registro Exitoso", "La actividad ha sido registrada.", "OK");
                await Navigation.PushAsync(new Activities());
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Alerta", "Error al registrar la actividad realizada", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.CapturePhotoAsync();

        if (foto != null)
        {
            var memoriaStream = await foto.OpenReadAsync();
            imgFoto.Source = ImageSource.FromStream(() => memoriaStream);
        }

    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();

        if (foto != null)
        {
            var memoriaStream = await foto.OpenReadAsync();
            imgFoto.Source = ImageSource.FromStream(() => memoriaStream);
        }

    }

    private async void Button_Editar(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new EditActivity(_selectedActivity));
    }

    private async void Button_Comment(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new Comments(_selectedActivity));
    }


}
