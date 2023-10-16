namespace Kino.ApiClient.Requests;

internal record LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}