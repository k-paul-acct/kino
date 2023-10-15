namespace Kino.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ImageUrl { get; set; }
    public int? RoleId { get; set; }

    public Role? Role { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<FaveList> FaveLists { get; set; } = new List<FaveList>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}