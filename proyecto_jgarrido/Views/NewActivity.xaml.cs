using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using proyecto_jgarrido.Models;
using proyecto_jgarrido.Services;
using System.Diagnostics;

namespace proyecto_jgarrido.Views;

public partial class NewActivity : ContentPage
{
    private HttpClient httpClient;
    public NewActivity()
	{
		InitializeComponent();
        httpClient = new HttpClient();
    }

    private async void OnCrearActividadClicked(object sender, EventArgs e)
    {
        try
        {
            var nuevaActividad = new NewActivityModel
            {
                Nombre = NombreEntry.Text,
                Descripcion = DescripcionEditor.Text,
                Duracion = Convert.ToInt32(DuracionEntry.Text),
                Categoria_Id = Convert.ToInt32(CategoryEntry.Text)
            };

            string json = System.Text.Json.JsonSerializer.Serialize(nuevaActividad);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine("JSON Enviado: " + json);

            HttpResponseMessage response = await httpClient.PostAsync("http://10.204.14.206:9000/activities", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "La actividad ha sido creada exitosamente.", "OK");
                await Navigation.PushAsync(new Activities());
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al crear la actividad.", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
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
