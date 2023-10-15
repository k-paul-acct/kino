namespace Kino.Domain.Models;

public class Title
{
    public int Id { get; set; }
    public string? TitleName { get; set; }
    public string? TitleAdditionalName { get; set; }
    public string? Description { get; set; }
    public int? Date { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<FaveList> FaveLists { get; set; } = new List<FaveList>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}