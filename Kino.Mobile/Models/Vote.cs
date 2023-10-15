namespace Kino.Mobile.Models;

public class Vote
{
    public int? Rating { get; set; }
    public int UserId { get; set; }
    public int TitleId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Title Title { get; set; } = null!;
}