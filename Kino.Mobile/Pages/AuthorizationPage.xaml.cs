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
                result += "������� �����!\n";

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
                result += "������� ������!\n";

            return result;
        }

        if (!string.IsNullOrEmpty(ValidateFields()))
        {
            await DisplayAlert("������", ValidateFields(), "OK");
            return;
        }

        Context.�urrentUser = await Context.apiClient.Login(tbLogin.Text, tbPassword.Text);

        if (Context.�urrentUser == null)
        {
            await DisplayAlert("������", "���� �� ������", "OK");
            return;
        }

        var element = Shell.Current.FindByName<Tab>("favorites");
        element.IsVisible = true;

        await Shell.Current.Navigation.PopAsync();
    }
}