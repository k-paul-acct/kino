namespace Kino.Api.Contracts.Requests;

public record CreateTitleRequest(
    string Name,
    string? AdditionalName,
    string? Description,
    int Year,
    string ImageUrl,
    IEnumerable<int> GenreIds);