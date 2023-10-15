namespace Kino.Domain.Models;

public class Genre
{
    public int Id { get; set; }
    public string? GenreName { get; set; }

    public ICollection<Title> Titles { get; set; } = new List<Title>();
    public ICollection<TitleHasGenre> TitleHasGenres { get; set; } = new List<TitleHasGenre>();
}