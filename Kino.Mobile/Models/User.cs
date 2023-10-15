namespace Kino.Mobile.Models;

public class User
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ImageUrl { get; set; }
    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<FaveList> FaveLists { get; set; } = new List<FaveList>();
    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}