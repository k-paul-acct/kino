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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            lbLogin.Text = Context.СurrentUser.Username;
            lbEmail.Text = Context.СurrentUser.Email;
            var source = Context.СurrentUser.ImageUrl ?? "pack://application:,,,/Kino.Desktop;component/Resources/Icons/icon_image.png";
            imgProfile.Source = new BitmapImage(new Uri(source));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthorizationPage());
            Context.СurrentUser = null;
        }

        private void btnSetPhoto_Click(object sender, RoutedEventArgs e)
        {
            //логика для сохранения фотки пользователя
            tbImgUrl.Text = string.Empty;
        }
    }
}
