namespace proyecto_jgarrido;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new Views.LoginPage());
    }
}

