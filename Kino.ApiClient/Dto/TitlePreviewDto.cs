namespace Kino.ApiClient.Dto;

public class TitlePreviewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AdditionalName { get; set; }
    public int Year { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int VotesNumber { get; set; }
    public IEnumerable<GenreDto> Genres { get; set; } = Enumerable.Empty<GenreDto>();
}