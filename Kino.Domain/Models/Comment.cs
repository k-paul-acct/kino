namespace Kino.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string? TextContent { get; set; }
    public DateTime Date { get; set; }
    public int? UserId { get; set; }
    public int? TitleId { get; set; }

    public User? User { get; set; }
    public Title? Title { get; set; }
}