using Kino.ApiClient.Dto;
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
    /// Логика взаимодействия для FavoritesPage.xaml
    /// </summary>
    public partial class FavoritesPage : Page
    {
        public FavoritesPage()
        {
            InitializeComponent();

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            lvMovies.ItemsSource = (await Context.apiClient.GetFavouritesTitles()).ToList();
        }

        private void lvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMovies.SelectedItem is TitlePreviewDto selectedMovie)
            {
                NavigationService.Navigate(new MoviePage(selectedMovie.Id));
            }
        }
    }
}
