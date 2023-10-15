namespace Kino.Mobile.Models;

public class Comment
{
    public int Id { get; set; }
    public string? TextContent { get; set; }
    public DateTime Date { get; set; }
    public int? UserId { get; set; }
    public int? TitleId { get; set; }

    public virtual User? User { get; set; }
    public virtual Title? Title { get; set; }
}