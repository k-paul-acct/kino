using Kino.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var result = string.Empty;
            string ValidateFields()
            {
                if (string.IsNullOrWhiteSpace(tbLogin.Text))
                    result += "Введите логин!\n";

                if (string.IsNullOrWhiteSpace(pbPassword.Password))
                    result += "Введите пароль!\n";

                return result;
            }

            if (!string.IsNullOrEmpty(ValidateFields()))
            {
                MessageBox.Show(ValidateFields(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Context.СurrentUser = await Context.apiClient.Login(tbLogin.Text, pbPassword.Password);

            if (Context.СurrentUser == null)
            {
                MessageBox.Show("Ввойти не удалось", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.NavigationService.Navigate(new ProfilePage());
        }

        private void btnToRegistr_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationPage());
        }

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPassword.Visibility = Visibility.Collapsed;
            pbPassword.Focus();
        }

        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password.Length == 0)
                tbPassword.Visibility = Visibility.Visible;
        }
    }
}
