namespace Kino.ApiClient.Requests;

internal class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}