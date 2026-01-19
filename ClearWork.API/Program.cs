using ClearWork.API.Extensions;
using ClearWork.API.Middlewares;
using ClearWork.Application.Extensions;
using ClearWork.Domain.Entities;
using ClearWork.Infrastructure.Extensions;
using ClearWork.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateAsyncScope();
var seeder = scope.ServiceProvider.GetRequiredService<IClearWorkSeeder>();
await seeder.SeedAsync();

app.UseSerilogRequestLogging();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGroup("/api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapControllers();

app.Run();