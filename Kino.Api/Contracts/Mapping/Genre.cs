using Kino.ApiClient.Dto;
using Kino.Domain.Models;

namespace Kino.Api.Contracts.Mapping;

public static partial class Mapping
{
    public static GenreDto MapToDto(this Genre genre)
    {
        return new GenreDto { Id = genre.Id, Name = genre.GenreName!, };
    }
}