namespace Kino.ApiClient.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public RoleDto Role { get; set; } = null!;
}