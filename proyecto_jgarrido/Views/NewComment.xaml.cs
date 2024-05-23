using System.Text;
using System.Text.Json;
using proyecto_jgarrido.Models;
using proyecto_jgarrido.Services;

namespace proyecto_jgarrido.Views;

public partial class NewComment : ContentPage
{
    private readonly HttpClient httpClient;
    private Actividad _selectedActivity;
    public NewComment(Actividad selectedActivity)
	{
        _selectedActivity = selectedActivity;
        InitializeComponent();
        httpClient = new HttpClient();
    }

    private async void OnCrearComentaryClicked(object sender, EventArgs e)
    {
        var authenticatedUser = UserSession.Instance.AuthenticatedUser;
        if (authenticatedUser == null)
        {
            await DisplayAlert("Error", "No se ha identificado al usuario.", "OK");
            return;
        }

        try
        {
            var registroComentario = new NewCommentModel
            {
                Usuario_Id = authenticatedUser.Id,
                Actividad_Id = _selectedActivity.Id,
                Comentario = ComentaryEditor.Text
            };

            string json = JsonSerializer.Serialize(registroComentario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await httpClient.PostAsync("http://10.204.14.206:9000/comments", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Registro Exitoso", "El comentario ha sido registrado.", "OK");
                await Navigation.PushAsync(new Comments(_selectedActivity));
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Alerta", "Error al registrar el comentario", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
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
