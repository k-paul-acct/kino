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
        var userApi = app.MapGroup("/users").WithTags("Users");

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
                        .AnyAsync(x => x.UserName == request.Username))
                {
                    return Results.Conflict();
                }

                context.Users.Add(user);
                await context.SaveChangesAsync();
                return Results.Ok();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return Results.Conflict();
            }
        });

        userApi.MapPut("", async (UpdateProfileRequest request, KinoDbContext context) =>
        {
            var user = await context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null) return Results.NotFound();

            try
            {
                user.ImageUrl = request.ImageUrl;
                await context.SaveChangesAsync();
                return Results.Ok(user.MapToDto());
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return Results.Conflict();
            }
        });

        userApi.MapDelete("/{id:int}", async (int id, KinoDbContext context) =>
        {
            var user = await context.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            try
            {
                user.RoleId = (int)Roles.Deleted;
                await context.SaveChangesAsync();
                return Results.Ok();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return Results.Conflict();
            }
        });
    }
}