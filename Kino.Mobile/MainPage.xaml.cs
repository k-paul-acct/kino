using Kino.Mobile.Pages;

namespace Kino.Mobile;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnLogin(object sender, EventArgs e)
    {
    }

    private async void OnRegister(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}