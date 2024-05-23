using System.Diagnostics;
using System.Net.Http;
using System.Text;
using proyecto_jgarrido.Models;

namespace proyecto_jgarrido.Views;

public partial class NewCategory : ContentPage
{
    private HttpClient httpClient;
    public NewCategory()
	{
		InitializeComponent();
        httpClient = new HttpClient();
    }

    private async void OnCrearCategoryClicked(object sender, EventArgs e)
    {
        try
        {
            var nuevaCategoria = new NewCategoryModel
            {
                Nombre = NombreEntry.Text,
                Descripcion = DescripcionEditor.Text
            };

            string json = System.Text.Json.JsonSerializer.Serialize(nuevaCategoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine("JSON Enviado: " + json);

            HttpResponseMessage response = await httpClient.PostAsync("http://10.2.3.102:9000/categories", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "La categoria ha sido creada exitosamente.", "OK");
                await Navigation.PushAsync(new Categories());
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al crear la categoria.", "Cerrar");
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

