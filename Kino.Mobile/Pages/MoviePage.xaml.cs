using Kino.ApiClient.Dto;
using Kino.Mobile.Models;

namespace Kino.Mobile.Pages;

public partial class MoviePage : ContentPage
{
    private TitleDetailsDto title { get; set; }
    private bool isFavorites { get; set; } = false;

    public MoviePage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        title = (await Context.apiClient.GetTitle(Context.CurrentMovieId))!;
        lbDate.Text = title.Year.ToString();
        lbName.Text = title.Name;
        lbDescription.Text = title.Description;
        lbRating.Text = title.Rating.ToString();
        imgTitle.Source = title.ImageUrl;
        cvGenres.ItemsSource = title.Genres;
        cvComments.ItemsSource = null;
        cvComments.ItemsSource = title.Comments;
        if (title.Comments.Any())
        {
            panelComment.IsVisible = true;
        }

        if (Context.СurrentUser == null)
        {
            panelAddRating.IsVisible = false;
            btnAddToFavorite.IsVisible = false;
            panelAddComment.IsVisible = false;
            return;
        }

        var fTitels = (await Context.apiClient.GetFavouritesTitles());
        if (fTitels.FirstOrDefault(x => x.Id == title.Id) != null)
        {
            btnAddToFavorite.Text = "Удалить из избранного";
            isFavorites = true;
        }
    }

    private async void btnAddToFavorite_Clicked(object sender, EventArgs e)
    {
        if (isFavorites)
        {
            await Context.apiClient.DeleteTitleFromFavourites(title.Id);
            btnAddToFavorite.Text = "Добавить в избранное";
            isFavorites = !isFavorites;
            return;
        }

        await Context.apiClient.AddTitleToFavourites(title.Id);
        btnAddToFavorite.Text = "Удалить из избранного";
        isFavorites = !isFavorites;
    }

    private void tbRating_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(tbRating.Text, out int number))
            tbRating.Text = string.Empty;

        if (int.TryParse(tbRating.Text, out int rating))
        {
            if (rating < 1)
                tbRating.Text = "1";
            else if (rating > 5)
                tbRating.Text = "5";
        }
    }

    private async void btnSubmitRating_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbRating.Text))
        {
            await Context.apiClient.RateTitle(title.Id, int.Parse(tbRating.Text));
            panelAddRating.IsVisible = false;
        }
    }

    private async void btnSubmitComment_Clicked(object sender, EventArgs e)
    {
        await Context.apiClient.AddCommentToTitle(title.Id, tbComment.Text);
        tbComment.Text = string.Empty;

        OnAppearing();
    }
}