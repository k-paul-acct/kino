using Kino.Mobile.Models;

namespace Kino.Mobile.Pages;

public partial class AuthorizationPage : ContentPage
{
	public AuthorizationPage()
	{
		InitializeComponent();
	}

    private async void btnSignUp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrationPage));
    }

    private async void btnSignIn_Clicked(object sender, EventArgs e)
    {
        var result = string.Empty;
        string ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
                result += "Введите логин!\n";

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
                result += "Введите пароль!\n";

            return result;
        }

        if (!string.IsNullOrEmpty(ValidateFields()))
        {
            await DisplayAlert("Ошибка", ValidateFields(), "OK");
            return;
        }

        Context.СurrentUser = await Context.apiClient.Login(tbLogin.Text, tbPassword.Text);

        if (Context.СurrentUser == null)
        {
            await DisplayAlert("Ошибка", "Вход не удался", "OK");
            return;
        }

        var element = Shell.Current.FindByName<Tab>("favorites");
        element.IsVisible = true;

        await Shell.Current.Navigation.PopAsync();
    }
}