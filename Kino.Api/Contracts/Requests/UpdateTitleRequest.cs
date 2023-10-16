namespace Kino.Api.Contracts.Requests;

public record UpdateTitleRequest(
    int Id,
    string Name,
    string? AdditionalName,
    string? Description,
    int Year,
    string ImageUrl,
    IEnumerable<int> GenreIds);