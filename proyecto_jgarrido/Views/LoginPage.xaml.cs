using Newtonsoft.Json;
using proyecto_jgarrido.Models;
using proyecto_jgarrido.Services;

namespace proyecto_jgarrido.Views;

public partial class LoginPage : ContentPage
{
    private const string Url = "http://10.2.3.102:9000/users";
    private readonly HttpClient cliente = new HttpClient();
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            MessageLabel.Text = "Por favor, ingrese su correo electrónico y contraseña.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Aquí deberías realizar la validación del usuario
        var user = await ValidateUser(email,password);

        if (user != null)
        {
            UserSession.Instance.SetAuthenticatedUser(user);
            // Navegar a la página principal (MainPage)
            await Navigation.PushAsync(new Activities());
        }
        else
        {
            MessageLabel.Text = "Correo electrónico o contraseña incorrectos.";
            MessageLabel.IsVisible = true;
        }
    }

    private async Task<Usuario> ValidateUser(string email, string password)
    {
        try
        {
            var response = await cliente.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            var usuariosJson = await response.Content.ReadAsStringAsync();
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(usuariosJson);

            foreach (var usuario in usuarios)
            {
                if (usuario.Email == email && usuario.Contraseña == password)
                {
                    return usuario;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al validar usuario: {ex.Message}");
        }

        return null;
    }
}
