using System.Text;
using Kino.ApiClient.Dto;
using Kino.ApiClient.Requests;
using Newtonsoft.Json;

namespace Kino.ApiClient;

public partial class ApiClient
{
    public async Task<UserDto?> Login(string username, string password)
    {
        var body = JsonConvert.SerializeObject(new LoginRequest { Username = username, Password = password, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("users/login", content);

        if (response.IsSuccessStatusCode)
        {
            var user = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());
            _userId = user!.Id;
            _authPart = $"userId={_userId}";
            return user;
        }

        _userId = null;
        _authPart = string.Empty;
        return null;
    }

    public async Task<bool> Register(string username, string email, string password)
    {
        var body = JsonConvert.SerializeObject(new RegisterRequest { Username = username, Email = email, Password = password, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("users/register", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<UserDto?> UpdateUserInfo(string imageUrl)
    {
        var body = JsonConvert.SerializeObject(new UpdateProfileRequest { Id = _userId ?? 0, ImageUrl = imageUrl, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PutAsync("users", content);

        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync())
            : null;
    }

    public async Task<bool> DeleteUser(int userId)
    {
        using var response = await _client.DeleteAsync($"users/{userId}");
        return response.IsSuccessStatusCode;
    }
}