namespace Kino.Domain.Models;

public class Vote
{
    public int? Rating { get; set; }
    public int UserId { get; set; }
    public int TitleId { get; set; }

    public User User { get; set; } = null!;
    public Title Title { get; set; } = null!;
}