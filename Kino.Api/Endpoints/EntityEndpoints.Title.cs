using Kino.Api.Contracts.Mapping;
using Kino.Api.Contracts.Requests;
using Kino.Api.Data;
using Kino.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Kino.Api.Endpoints;

public static partial class EntityEndpoints
{
    private static async Task<(Title Title, int VotesNumber)> GetTitleForPreview(int id, KinoDbContext context)
    {
        var title = await context.Titles
            .Include(x => x.Genres)
            .Select(x => new { Title = x, VotesNumber = x.Votes.Count, })
            .FirstOrDefaultAsync(x => x.Title.Id == id);
        return title is null ? default : (title.Title, title.VotesNumber);
    }

    private static Task<(Title Title, int VotesNumber)?> GetTitleForDetails(int id, KinoDbContext context)
    {
        throw new NotImplementedException();
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

                var preview = await GetTitleForPreview(title.Id, context);
                return Results.Ok(preview!.Title.MapToPreviewDto(preview.VotesNumber));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.Conflict();
            }
        });
    }
}