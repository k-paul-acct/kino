using Kino.ApiClient.Dto;
using Newtonsoft.Json;

namespace Kino.ApiClient;

public partial class ApiClient
{
    public async Task<IEnumerable<GenreDto>> GetAllGenres()
    {
        using var response = await _client.GetAsync("genres");
        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<IEnumerable<GenreDto>>(await response.Content.ReadAsStringAsync())!
            : Enumerable.Empty<GenreDto>();
    }
}