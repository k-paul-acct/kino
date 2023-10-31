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
                result += "������� �����.\n";

            if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
                result += "������� ���������� email.\n";

            if (tbPassword.Text.Length < 8)
                result += "������ ������ ���� ������� 8 ��������";

            if (string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(tbPasswordConfirm.Text))
                result += "������� ������ � ��� ����.";
            else if (tbPassword.Text != tbPasswordConfirm.Text)
                result += "������ �� ���������.\n";

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
            await DisplayAlert("������", ValidateFields(), "OK");
            return;
        }

        var result = await Context.apiClient.Register(tbLogin.Text, tbEmail.Text, tbPassword.Text);
        if (result)
        {
            await DisplayAlert("�������", "�� ������������������ � �������", "OK");
            await Shell.Current.Navigation.PopAsync();
            return;
        }

        await DisplayAlert("������", "������������������ ��� ����� ������� �� �������", "OK");
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}