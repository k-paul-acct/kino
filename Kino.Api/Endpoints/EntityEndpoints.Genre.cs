using Kino.Api.Contracts.Mapping;
using Kino.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Kino.Api.Endpoints;

public static partial class EntityEndpoints
{
    private static void MapGenreEndpoints(this IEndpointRouteBuilder app)
    {
        var userApi = app.MapGroup("/genres").WithTags("Genres");

        userApi.MapGet("", async (KinoDbContext context) =>
        {
            var genres = await context.Genres.ToListAsync();
            return Results.Ok(genres.Select(x => x.MapToDto()));
        });
    }
}