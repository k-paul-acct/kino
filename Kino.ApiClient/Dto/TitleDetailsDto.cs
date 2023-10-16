namespace Kino.ApiClient.Dto;

public class TitleDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AdditionalName { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
    public string ImageUrl { get; set; } = null!;
    public double? Rating { get; set; }
    public int VotesNumber { get; set; }
    public IEnumerable<GenreDto> Genres { get; set; } = Enumerable.Empty<GenreDto>();
    public IEnumerable<CommentDto> Comments { get; set; } = Enumerable.Empty<CommentDto>();
}