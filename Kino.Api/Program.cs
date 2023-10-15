using Kino.Api.Data;
using Kino.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KinoDbContext>(o => o.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEntityEndpoints();

try
{
    using var scope = app.Services.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<KinoDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}
catch (Exception e)
{
    Console.WriteLine($"No DB. Cannot create.\nException: {e}");
}

app.Run();