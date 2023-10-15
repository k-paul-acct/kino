namespace Kino.Mobile.Models;

public class FaveList
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? TitleId { get; set; }

    public virtual User? User { get; set; }
    public virtual Title? Title { get; set; }
}