using Kino.ApiClient.Dto;
using Kino.Desktop.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
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
    /// Логика взаимодействия для MoviePage.xaml
    /// </summary>
    public partial class MoviePage : Page
    {
        private TitleDetailsDto title { get; set; }
        private bool isFavorites { get; set; } = false;

        public MoviePage(int id)
        {
            InitializeComponent();
            InitializeComponentAsync(id);
        }

        private async void InitializeComponentAsync(int id)
        {
            title = (await Context.apiClient.GetTitle(id))!;
            lbDate.Text = title.Year.ToString();
            lbName.Text = title.Name;
            lbDescription.Text = title.Description;
            lbRating.Text = title.Rating.ToString();
            imgTitle.Source = new BitmapImage(new Uri(title.ImageUrl));
            icGenres.ItemsSource = title.Genres;
            icComments.ItemsSource = title.Comments;
            if (title.Comments.Any())
            {
                panelComment.Visibility = Visibility.Visible;
            }

            if (Context.СurrentUser == null)
            {
                panelAddRating.Visibility = Visibility.Collapsed;
                panelAction.Visibility = Visibility.Collapsed;
                panelAddComment.Visibility = Visibility.Collapsed;
                return;
            }

            if (Context.СurrentUser.Role.Id == 1)
            {
                btnDeleteTitle.Visibility = Visibility.Collapsed;
                btnEditTitle.Visibility = Visibility.Collapsed;
            }

            var fTitels = (await Context.apiClient.GetFavouritesTitles());
            if (fTitels.FirstOrDefault(x => x.Id == title.Id) != null)
            {
                btnAddToFavorite.Content = "Удалить из избранного";
                isFavorites = true;
            }
        }

        private async void btnSubmitComment_Click(object sender, RoutedEventArgs e)
        {
            await Context.apiClient.AddCommentToTitle(title.Id, tbComment.Text);
            tbComment.Text = string.Empty;

            InitializeComponentAsync(title.Id);
        }

        private async void btnAddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (isFavorites)
            {
                await Context.apiClient.DeleteTitleFromFavourites(title.Id);
                btnAddToFavorite.Content = "Добавить в избранное";
                isFavorites = !isFavorites;
                return;
            }

            await Context.apiClient.AddTitleToFavourites(title.Id);
            btnAddToFavorite.Content = "Удалить из избранного";
            isFavorites = !isFavorites;
        }

        private void btnEditTitle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddMoviePage(title.Id));
        }

        private async void btnDeleteTitle_Click(object sender, RoutedEventArgs e)
        {
            await Context.apiClient.DeleteTitle(title.Id);
            NavigationService.GoBack();
        }

        private void tbRating_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(tbRating.Text, out int rating))
            {
                if (rating < 1)
                    tbRating.Text = "1"; 
                else if (rating > 5)
                    tbRating.Text = "5";
            }
        }

        private void tbRating_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int number))
                e.Handled = true;
        }

        private async void btnSubmitRating_Click(object sender, RoutedEventArgs e)
        {
            if (!tbRating.Text.IsNullOrEmpty())
            {
                await Context.apiClient.RateTitle(title.Id, int.Parse(tbRating.Text));
                panelAddRating.Visibility = Visibility.Collapsed;
            }
        }
    }
}
