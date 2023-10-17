using Kino.ApiClient.Dto;
using Kino.ApiClient.Requests;
using Kino.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Kino.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddMoviePage.xaml
    /// </summary>
    public partial class AddMoviePage : Page
    {
        HashSet<int> genres = new HashSet<int>();
        List<GenreDto> genresActual = new List<GenreDto>();
        List<GenreDto> genresInfo = new List<GenreDto>();
        public AddMoviePage()
        {
            InitializeComponent();

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            genresInfo = (await Context.apiClient.GetAllGenres()).ToList();
            cbCategories.ItemsSource = genresInfo;
        }

        private void btnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            if (cbCategories.SelectedIndex == -1)
                return;
            var id = cbCategories.SelectedIndex + 1;
            if (!genres.Contains(id))
            {
                genresActual.Add(genresInfo.First(x => x.Id == id));
            }
            genres.Add(id);

            icGenres.ItemsSource = null;
            icGenres.ItemsSource = genresActual;
        }

        private void btnSetPhoto_Click(object sender, RoutedEventArgs e)
        {
            imgMovie.Source = new BitmapImage(new Uri(tbImgUrl.Text));
        }

        private async void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            var titel = new CreateTitleRequest()
            {
                Name = tbName.Text,
                Description = tbDescription.Text,
                ImageUrl = tbImgUrl.Text,
                GenreIds = genres,
                Year = int.Parse(tbYear.Text),
            };


            await Context.apiClient.CreateTitle(titel);
            MessageBox.Show("Вы добавили фильм на площадку", "Успешно", MessageBoxButton.OK);
            NavigationService.Navigate(new MovieListPage());
        }

        private void btnResetGenre_Click(object sender, RoutedEventArgs e)
        {
            icGenres.ItemsSource = null;
            genres.Clear();
            genresActual.Clear();
        }
    }
}
