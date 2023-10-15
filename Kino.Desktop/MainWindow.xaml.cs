using Kino.Desktop.Models;
using Kino.Desktop.Views;
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

namespace Kino.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainFrame.NavigationService.Navigate(new MovieListPage());
            //if (Context.UserNow == null)
            //{
            //    tbBtnExit.Visibility = Visibility.Collapsed;
            //}
        }

        private void btnShowMovieListPage_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new MovieListPage());
        }

        private void btnFavorites_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new FavoritesPage());
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new AddMoviePage());
        }

        private void btnToProfilePage_Click(object sender, RoutedEventArgs e)
        {
            if (Context.IsAuth)
            {
                mainFrame.NavigationService.Navigate(new ProfilePage());
            }
            mainFrame.NavigationService.Navigate(new AuthorizationPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new MovieListPage());
        }
    }
}
