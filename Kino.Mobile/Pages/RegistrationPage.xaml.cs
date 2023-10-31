using Kino.Mobile.Models;
using System.Text.RegularExpressions;

namespace Kino.Mobile.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        string ValidateFields()
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
                result += "Введите логин.\n";

            if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
                result += "Введите корректный email.\n";

            if (tbPassword.Text.Length < 8)
                result += "Пароль должен быть минимум 8 символов";

            if (string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(tbPasswordConfirm.Text))
                result += "Введите пароль в оба поля.";
            else if (tbPassword.Text != tbPasswordConfirm.Text)
                result += "Пароли не совпадают.\n";

            return result;
        }

        bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Match match = Regex.Match(email, pattern);
            return match.Success;
        }

        if (!string.IsNullOrEmpty(ValidateFields()))
        {
            await DisplayAlert("Ошибка", ValidateFields(), "OK");
            return;
        }

        var result = await Context.apiClient.Register(tbLogin.Text, tbEmail.Text, tbPassword.Text);
        if (result)
        {
            await DisplayAlert("Успешно", "Вы зарегистрировались в системе", "OK");
            await Shell.Current.Navigation.PopAsync();
            return;
        }

        await DisplayAlert("Ошибка", "Зарегистрироваться под таким логином не удалось", "OK");
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}