using System.Text;
using Kino.ApiClient.Dto;
using Kino.ApiClient.Requests;
using Newtonsoft.Json;

namespace Kino.ApiClient;

public partial class ApiClient
{
    public async Task<TitleDetailsDto?> CreateTitle(CreateTitleRequest request)
    {
        var body = JsonConvert.SerializeObject(request);
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("titles", content);

        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<TitleDetailsDto>(await response.Content.ReadAsStringAsync())
            : null;
    }

    public async Task<TitleDetailsDto?> UpdateTitle(UpdateTitleRequest request)
    {
        var body = JsonConvert.SerializeObject(request);
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PutAsync("titles", content);

        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<TitleDetailsDto>(await response.Content.ReadAsStringAsync())
            : null;
    }

    public async Task<IEnumerable<TitlePreviewDto>> GetTitles(string? query, IEnumerable<int> genreIds)
    {
        var genres = string.Join("&", genreIds.Select(x => $"genreIds={x}"));
        using var response = await _client.GetAsync($"titles?query={query}&{genres}");
        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<IEnumerable<TitlePreviewDto>>(await response.Content.ReadAsStringAsync())!
            : Enumerable.Empty<TitlePreviewDto>();
    }

    public async Task<TitleDetailsDto?> GetTitle(int titleId)
    {
        using var response = await _client.GetAsync($"titles/{titleId}?{_authPart}");
        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<TitleDetailsDto>(await response.Content.ReadAsStringAsync())
            : null;
    }

    public async Task<bool> DeleteTitle(int titleId)
    {
        using var response = await _client.DeleteAsync($"titles/{titleId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddTitleToFavourites(int titleId)
    {
        using var response = await _client.PostAsync($"titles/{titleId}/add-to-favourites?{_authPart}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTitleFromFavourites(int titleId)
    {
        using var response = await _client.DeleteAsync($"titles/{titleId}/remove-from-favourites?{_authPart}");
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<TitlePreviewDto>> GetFavouritesTitles()
    {
        using var response = await _client.GetAsync($"titles/favourites?{_authPart}");
        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<IEnumerable<TitlePreviewDto>>(await response.Content.ReadAsStringAsync())!
            : Enumerable.Empty<TitlePreviewDto>();
    }

    public async Task<CommentDto?> AddCommentToTitle(int titleId, string text)
    {
        var body = JsonConvert.SerializeObject(new CommentRequest { Text = text, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync($"titles/{titleId}/comment?{_authPart}", content);
        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<CommentDto>(await response.Content.ReadAsStringAsync())!
            : null;
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        using var response = await _client.DeleteAsync($"titles/comments/{commentId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RateTitle(int titleId, int rate)
    {
        var body = JsonConvert.SerializeObject(new VoteRequest { Rating = rate, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync($"titles/{titleId}/vote?{_authPart}", content);
        return response.IsSuccessStatusCode;
    }
}