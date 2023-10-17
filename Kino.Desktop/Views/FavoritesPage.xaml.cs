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
        private HashSet<int> genres = new HashSet<int>();
        public FavoritesPage()
        {
            InitializeComponent();

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            cbCategories.ItemsSource = (await Context.apiClient.GetAllGenres()).ToList();
            lvMovies.ItemsSource = (await Context.apiClient.GetFavouritesTitles()).ToList();
        }

        private void lvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMovies.SelectedItem is TitlePreviewDto selectedMovie)
            {
                NavigationService.Navigate(new MoviePage(selectedMovie.Id));
            }
        }

        private async void btnSearchReset_Click(object sender, RoutedEventArgs e)
        {
            cbCategories.SelectedIndex = -1;
            cbSortMode.SelectedIndex = -1;
            tbSearch.Text = string.Empty;
            lvMovies.ItemsSource = null;
            lvMovies.ItemsSource = (await Context.apiClient.GetTitles("", genres)).ToList();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            genres.Add(cbCategories.SelectedIndex + 1);
            lvMovies.ItemsSource = null;
            lvMovies.ItemsSource = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).ToList();
            genres.Clear();
        }
    }
}
