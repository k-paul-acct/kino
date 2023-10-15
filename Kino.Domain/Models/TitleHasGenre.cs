namespace Kino.Domain.Models;

public class TitleHasGenre
{
    public int GenreId { get; set; }
    public int TitleId { get; set; }

    public Genre Genre { get; set; } = null!;
    public Title Title { get; set; } = null!;
}