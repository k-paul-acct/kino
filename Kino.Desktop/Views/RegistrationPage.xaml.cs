using Kino.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kino.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void tbPassword1_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPassword1.Visibility = Visibility.Collapsed;
            pbPassword1.Focus();
        }

        private void pbPassword1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbPassword1.Password.Length == 0)
                tbPassword1.Visibility = Visibility.Visible;
        }

        private void tbPassword2_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPassword2.Visibility = Visibility.Collapsed;
            pbPassword2.Focus();
        }

        private void pbPassword2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbPassword2.Password.Length == 0)
                tbPassword2.Visibility = Visibility.Visible;
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string ValidateFields()
            {
                string result = string.Empty;
                if (string.IsNullOrWhiteSpace(tbLogin.Text))
                    result += "Введите логин.\n";

                if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
                    result += "Введите корректный email.\n";

                if (pbPassword1.Password.Length < 8)
                    result += "Пароль должен быть минимум 8 символов";

                if (string.IsNullOrWhiteSpace(pbPassword1.Password) || string.IsNullOrWhiteSpace(pbPassword2.Password))
                    result += "Введите пароль в оба поля.";
                else if (pbPassword1.Password != pbPassword2.Password)
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
                MessageBox.Show(ValidateFields(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = await Context.apiClient.Register(tbLogin.Text, tbEmail.Text, pbPassword1.Password);
            if (result)
            {
                MessageBox.Show("Вы зарегистрировались в ситеме", "Успешно", MessageBoxButton.OK);
                this.NavigationService.GoBack();
                return;
            }

            MessageBox.Show("Ошибка");

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
