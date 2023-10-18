namespace Kino.Mobile.Pages;

public partial class MovieListPage : ContentPage
{
	public MovieListPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AuthorizationPage());
    }
}