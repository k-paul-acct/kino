namespace Kino.ApiClient.Requests;

public class CreateTitleRequest
{
    public string Name { get; set; } = null!;
    public string? AdditionalName { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
    public string ImageUrl { get; set; } = null!;
    public IEnumerable<int> GenreIds { get; set; } = Enumerable.Empty<int>();
}