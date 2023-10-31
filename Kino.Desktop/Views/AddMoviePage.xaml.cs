using Kino.ApiClient.Dto;
using Kino.ApiClient.Requests;
using Kino.Desktop.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private HashSet<int> genres = new HashSet<int>();
        private List<GenreDto> genresActual = new List<GenreDto>();
        private List<GenreDto> genresInfo = new List<GenreDto>();
        private TitleDetailsDto title { get; set; }
        private bool isEdit = false;

        public AddMoviePage()
        {
            InitializeComponent();

            InitializeAsync();
        }

        public AddMoviePage(int id)
        {
            InitializeComponent();

            EditInitializeAsync(id);
        }

        private async void EditInitializeAsync(int id)
        {
            isEdit = true;
            title = (await Context.apiClient.GetTitle(id))!;
            tbYear.Text = title.Year.ToString();
            tbName.Text = title.Name;
            tbDescription.Text = title.Description;
            imgMovie.Source = new BitmapImage(new Uri(title.ImageUrl));
            icGenres.ItemsSource = title.Genres;
            genresActual = title.Genres.ToList();
            genresInfo = (await Context.apiClient.GetAllGenres()).ToList();
            cbCategories.ItemsSource = genresInfo;
            btnAddMovie.Content = "Сохранить изменения";

            foreach (var genre in genresActual)
                genres.Add(genre.Id);
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
            if (!tbImgUrl.Text.IsNullOrEmpty())
                imgMovie.Source = new BitmapImage(new Uri(tbImgUrl.Text));
        }

        private async void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = string.Empty;

            if (tbName.Text.IsNullOrEmpty() || tbDescription.Text.IsNullOrEmpty() || tbYear.Text.IsNullOrEmpty())
                errorMessage += "Заполните все поля\n";

            int a;
            if (!(int.TryParse(tbYear.Text, out a) && a > 1895 && a < DateTime.Now.Year))
                errorMessage += "Неккоректная дата у фильма\n";

            if (tbImgUrl.Text.IsNullOrEmpty() && isEdit == false)
                errorMessage += "Установите фото";

            if (!errorMessage.IsNullOrEmpty())
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!isEdit)
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
                return;
            }

            var titleUpdate = new UpdateTitleRequest()
            {
                Id = title.Id,
                Name = tbName.Text,
                Description = tbDescription.Text,
                ImageUrl = title.ImageUrl ?? tbImgUrl.Text,
                GenreIds = genres,
                Year = int.Parse(tbYear.Text),
            };

            await Context.apiClient.UpdateTitle(titleUpdate);
            NavigationService.Navigate(new MoviePage(title.Id));
            NavigationService.RemoveBackEntry();
        }

        private void btnResetGenre_Click(object sender, RoutedEventArgs e)
        {
            icGenres.ItemsSource = null;
            genres.Clear();
            genresActual.Clear();
        }

        private void tbYear_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text))
            {
                e.Handled = true; 
            }
        }

        private bool IsNumericInput(string text)
        {
            return Regex.IsMatch(text, "^[0-9]+$");
        }
    }
}
