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
    /// Логика взаимодействия для MoviePage.xaml
    /// </summary>
    public partial class MoviePage : Page
    {
        private TitleDetailsDto title { get; set; }
        public MoviePage(int id)
        {
            InitializeComponent();
            InitializeComponentAsync(id);
        }

        private async void InitializeComponentAsync(int id)
        {
            title = await Context.apiClient.GetTitle(id);
            lbDate.Text = title.Year.ToString();
            lbName.Text = title.Name;
            lbDescription.Text = title.Description;
            lbRating.Text = title.Rating.ToString();
            imgTitle.Source = new BitmapImage(new Uri(title.ImageUrl));
            icGenres.ItemsSource = title.Genres;
            icComments.ItemsSource = title.Comments;
        }

        private async void btnSubmitComment_Click(object sender, RoutedEventArgs e)
        {
            await Context.apiClient.AddCommentToTitle(title.Id, tbComment.Text);
            tbComment.Text = string.Empty;

            InitializeComponentAsync(title.Id);
        }
    }
}
