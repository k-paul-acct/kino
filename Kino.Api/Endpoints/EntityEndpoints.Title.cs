using Kino.Api.Contracts.Requests;
using Kino.Api.Data;
using Kino.ApiClient.Dto;
using Kino.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Kino.Api.Endpoints;

public static partial class EntityEndpoints
{
    private static IQueryable<TitlePreviewDto> GetTitlePreviewDtos(this IQueryable<Title> titles)
    {
        return titles.Select(x => new TitlePreviewDto
        {
            Id = x.Id,
            Name = x.TitleName!,
            AdditionalName = x.TitleAdditionalName,
            Year = x.Date!.Value,
            ImageUrl = x.ImageUrl!,
            VotesNumber = x.Votes.Count,
            Genres = x.Genres.Select(y => new GenreDto
            {
                Id = y.Id,
                Name = y.GenreName!,
            }),
        });
    }

    private static IQueryable<TitleDetailsDto> GetTitleDetailsDtos(this IQueryable<Title> titles)
    {
        return titles.Select(x => new TitleDetailsDto
        {
            Id = x.Id,
            Name = x.TitleName!,
            AdditionalName = x.TitleAdditionalName,
            Description = x.Description,
            Year = x.Date!.Value,
            ImageUrl = x.ImageUrl!,
            VotesNumber = x.Votes.Count,
            Comments = x.Comments.Select(y => new CommentDto
            {
                Id = y.Id,
                TitleId = y.TitleId!.Value,
                Date = y.Date,
                Text = y.TextContent!,
                Username = y.User!.UserName!,
                UserId = y.UserId!.Value,
            }),
            Genres = x.Genres.Select(y => new GenreDto
            {
                Id = y.Id,
                Name = y.GenreName!,
            }),
            Rating = x.Votes.Average(y => y.Rating),
        });
    }

    private static IQueryable<Title> FilterByGenres(this IQueryable<Title> titles, int[]? genreIds)
    {
        return genreIds.IsNullOrEmpty() ? titles : titles.Where(x => x.Genres.Any(y => genreIds!.Contains(y.Id)));
    }

    private static IQueryable<Title> FilterByQuery(this IQueryable<Title> titles, string? query)
    {
        return query.IsNullOrEmpty()
            ? titles
            : titles.Where(x => x.TitleName!.Contains(query!) ||
                                (x.TitleAdditionalName != null &&
                                 x.TitleAdditionalName.Contains(query!)));
    }

    private static void MapTitleEndpoints(this IEndpointRouteBuilder app)
    {
        var userApi = app.MapGroup("/titles").WithTags("Titles");

        userApi.MapPost("", async (CreateTitleRequest request, KinoDbContext context) =>
        {
            var title = new Title
            {
                TitleName = request.Name,
                TitleAdditionalName = request.AdditionalName,
                Description = request.Description,
                Date = request.Year,
                ImageUrl = request.ImageUrl,
            };

            try
            {
                context.Titles.Add(title);
                await context.SaveChangesAsync();

                var genres = request.GenreIds.Select(x => new TitleHasGenre { GenreId = x, TitleId = title.Id, });
                context.TitleHasGenres.AddRange(genres);
                await context.SaveChangesAsync();

                var preview = await context.Titles.GetTitlePreviewDtos().FirstOrDefaultAsync(x => x.Id == title.Id);
                return Results.Ok(preview);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.Conflict();
            }
        });

        userApi.MapGet("", async (KinoDbContext context, int[]? genreIds, string? query) =>
        {
            var result = await context.Titles
                .FilterByGenres(genreIds)
                .FilterByQuery(query)
                .GetTitlePreviewDtos()
                .ToListAsync();

            return Results.Ok(result);
        });

        userApi.MapGet("/{id:int}", async (int id, KinoDbContext context) =>
        {
            var title = await context.Titles.GetTitleDetailsDtos().FirstOrDefaultAsync(x => x.Id == id);
            return title is null ? Results.NotFound() : Results.Ok(title);
        });
    }
}