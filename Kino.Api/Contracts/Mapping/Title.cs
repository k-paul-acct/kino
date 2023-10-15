using Kino.ApiClient.Dto;
using Kino.Domain.Models;

namespace Kino.Api.Contracts.Mapping;

public static partial class Mapping
{
    public static TitlePreviewDto MapToPreviewDto(this Title title, int votesNumber)
    {
        return new TitlePreviewDto
        {
            Id = title.Id,
            Name = title.TitleName!,
            AdditionalName = title.TitleAdditionalName,
            Year = title.Date!.Value,
            ImageUrl = title.ImageUrl!,
            VotesNumber = votesNumber,
            Genres = title.Genres.Select(x => x.MapToDto()),
        };
    }

    public static TitleDetailsDto MapToDetailsDto(this Title title)
    {
        return new TitleDetailsDto { };
    }
}