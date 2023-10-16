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
            UserId = user!.Id;
        }

        UserId = null;
        return null;
    }

    public async Task<bool> Register(string username, string email, string password)
    {
        var body = JsonConvert.SerializeObject(new RegisterRequest { Username = username, Email = email, Password = password, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("users/register", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(string username, string email, string password)
    {
        var body = JsonConvert.SerializeObject(new RegisterRequest { Username = username, Email = email, Password = password, });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("users/register", content);

        return response.IsSuccessStatusCode;
    }
}