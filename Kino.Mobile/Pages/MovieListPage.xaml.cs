using Kino.ApiClient.Dto;
using Kino.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kino.Mobile.Pages;

public partial class MovieListPage : ContentPage
{
    private HashSet<int> genres = new HashSet<int>();
    public MovieListPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        pickerCategory.ItemsSource = await SetCategory();
        cvMovie.ItemsSource = (await Context.apiClient.GetTitles("", genres)).ToList();
        pickerSortMode.SelectedIndex = 0;
        pickerCategory.SelectedIndex = 0;
    }

    private async void cvMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cvMovie.SelectedItem is TitlePreviewDto selectedMovie)
        {
            Context.CurrentMovieId = selectedMovie.Id;
            await Shell.Current.GoToAsync(nameof(MoviePage));
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        filterContainer.IsVisible = !filterContainer.IsVisible;
    }

    private async void btnSearch_Clicked(object sender, EventArgs e)
    {
        if (!(pickerCategory.SelectedIndex == 0))
        {
            genres.Add(pickerCategory.SelectedIndex);
        }

        cvMovie.ItemsSource = null;
        var list = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).ToList();
        cvMovie.ItemsSource = list;
        genres.Clear();

        switch (pickerSortMode.SelectedIndex)
        {
            case 1:
                cvMovie.ItemsSource = null;
                cvMovie.ItemsSource = list.OrderBy(movie => movie.Year).ToList();
                break;
            case 0:
                cvMovie.ItemsSource = null;
                cvMovie.ItemsSource = list.OrderByDescending(movie => movie.Year).ToList();
                break;
        }
    }

    private async void btnResetSearch_Clicked(object sender, EventArgs e)
    {
        pickerSortMode.SelectedIndex = 0;
        pickerCategory.SelectedIndex = 0;
        tbSearch.Text = string.Empty;
        cvMovie.ItemsSource = null;
        cvMovie.ItemsSource = (await Context.apiClient.GetTitles("", genres)).ToList();
    }

    private async void pickerSortMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (pickerSortMode.SelectedIndex)
        {
            case 0:
                cvMovie.ItemsSource = null;
                cvMovie.ItemsSource = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).OrderBy(movie => movie.Year).ToList();
                break;
            case 1:
                cvMovie.ItemsSource = null;
                cvMovie.ItemsSource = (await Context.apiClient.GetTitles(tbSearch.Text, genres)).OrderByDescending(movie => movie.Year).ToList();
                break;
        }
    }

    private async Task<List<string>> SetCategory()
    {
        var listCategories = new List<string>() {"Æàíð"};
        var list = (await Context.apiClient.GetAllGenres()).ToList();

        foreach (var category in list)
        {
            listCategories.Add(category.Name);
        }
        return listCategories;
    }
}