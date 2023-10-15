namespace Kino.Api.Endpoints;

public static partial class EntityEndpoints
{
    public static IEndpointRouteBuilder MapEntityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api");
        group.MapUserEndpoints();
        return app;
    }
}