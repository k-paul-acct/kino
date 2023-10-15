using Kino.Api.Contracts.Mapping;
using Kino.Api.Contracts.Requests;
using Kino.Api.Data;
using Kino.Domain.Models;
using Kino.Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace Kino.Api.Endpoints;

public static partial class EntityEndpoints
{
    private static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userApi = app.MapGroup("/users");

        userApi.MapPost("/login", async (LoginRequest request, KinoDbContext context) =>
        {
            var user = await context.Users
                .Where(x => x.RoleId != (int)Roles.Deleted)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserName == request.Username && x.Password == request.Password);
            return user is null ? Results.Unauthorized() : Results.Ok(user.MapToDto());
        });

        userApi.MapPost("/register", async (RegisterRequest request, KinoDbContext context) =>
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                Password = request.Password,
                RoleId = (int)Roles.User,
            };

            try
            {
                if (await context.Users
                        .Where(x => x.RoleId != (int)Roles.Deleted)
                        .AnyAsync(x => x.UserName == request.Username)) return Results.Conflict();
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return Results.Conflict();
            }

            return Results.Ok();
        });
    }
}