using Kino.ApiClient.Dto;
using Kino.ApiClient.Requests;
using Kino.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            var window = Application.Current.MainWindow;
            if (window != null)
            {
                Button btnAddMovie = FindVisualChild<Button>(window, "btnAddMovie")!;
                Button btnFavorites = FindVisualChild<Button>(window, "btnFavorites")!;

                if (btnAddMovie != null) btnAddMovie.Visibility = Visibility.Collapsed;
                if (btnFavorites != null) btnFavorites.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnSetPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbImgUrl.Text))
                return;

            var provile = new UpdateProfileRequest()
            {
                Id = Context.СurrentUser.Id,
                ImageUrl = tbImgUrl.Text,
            };

            await Context.apiClient.UpdateUserInfo(tbImgUrl.Text);
            imgProfile.Source = new BitmapImage(new Uri(tbImgUrl.Text));
            tbImgUrl.Text = string.Empty;

        }

        private T? FindVisualChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                    return typedChild;
                else
                {
                    var result = FindVisualChild<T>(child, childName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
