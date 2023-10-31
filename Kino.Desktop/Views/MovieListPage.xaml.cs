using Kino.ApiClient.Dto;
using Kino.ApiClient.Requests;
using Kino.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Kino.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для MovieListPage.xaml
    /// </summary>
    public partial class MovieListPage : Page
    {
        private HashSet<int> genres = new HashSet<int>();
        public MovieListPage()
        {
            InitializeComponent();

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            cbCategories.ItemsSource = (await Context.apiClient.GetAllGenres()).ToList();
            lvMovies.ItemsSource = (await Context.apiClient.GetTitles("", genres)).ToList();
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
            var list = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).ToList();
            lvMovies.ItemsSource = list;
            genres.Clear();

            switch (cbSortMode.SelectedIndex)
            {
                case 0:
                    lvMovies.ItemsSource = null;
                    lvMovies.ItemsSource = list.OrderBy(movie => movie.Year).ToList();
                    break;
                case 1:
                    lvMovies.ItemsSource = null;
                    lvMovies.ItemsSource = list.OrderByDescending(movie => movie.Year).ToList();
                    break;
            }
        }

        private async void cbSortMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSortMode.SelectedIndex)
            {
                case 0:
                    lvMovies.ItemsSource = null;
                    lvMovies.ItemsSource = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).OrderBy(movie => movie.Year).ToList();
                    break;
                case 1:
                    lvMovies.ItemsSource = null;
                    lvMovies.ItemsSource = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).OrderByDescending(movie => movie.Year).ToList();
                    break;
            }
        }
    }
}
