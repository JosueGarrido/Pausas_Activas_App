using System.Text;
using Newtonsoft.Json;
using proyecto_jgarrido.Models;

namespace proyecto_jgarrido.Views;

public partial class EditActivity : ContentPage
{
    private Actividad _selectedActivity;
    public EditActivity(Actividad datos)
	{
		InitializeComponent();
        _selectedActivity = datos;
        IdEntry.Text = datos.Id.ToString();
        NombreEntry.Text = datos.Nombre.ToString();
        DescripcionEditor.Text = datos.Descripcion.ToString();
        DuracionEntry.Text = datos.Duracion.ToString();
        CategoryEntry.Text = datos.Categoria_Id.ToString();
        FechaEntry.Text = datos.Fecha_Creacion.ToString();
    }


    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    Id = IdEntry.Text,
                    Nombre = NombreEntry.Text,
                    Descripcion = DescripcionEditor.Text,
                    Duracion = DuracionEntry.Text,
                    Categoria_Id = CategoryEntry.Text,
                    Fecha_Creacion = FechaEntry.Text
                }), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("http://10.2.3.102:9000/activities/" + _selectedActivity.Id, content);

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new Activities());
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Alerta", $"Error al actualizar la actividad: {responseString}", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://10.2.3.102:9000/activities/{_selectedActivity.Id}");

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new Activities());
                }
                else
                {
                    await DisplayAlert("Alerta", "Error al eliminar la actividad", "Cerrar");
                }
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
