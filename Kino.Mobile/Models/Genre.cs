namespace Kino.Mobile.Models;

public class Genre
{
    public int Id { get; set; }
    public string? GenreName { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}