using Kino.ApiClient.Dto;
using Kino.Mobile.Models;

namespace Kino.Mobile.Pages;

public partial class FavoritesPage : ContentPage
{
    private HashSet<int> genres = new HashSet<int>();
    public FavoritesPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        cvMovie.ItemsSource = (await Context.apiClient.GetFavouritesTitles()).ToList();
    }

    private async void cvMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cvMovie.SelectedItem is TitlePreviewDto selectedMovie)
        {
            Context.CurrentMovieId = selectedMovie.Id;
            await Shell.Current.GoToAsync(nameof(MoviePage));
        }
    }
}