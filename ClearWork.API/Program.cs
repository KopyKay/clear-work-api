using ClearWork.API.Extensions;
using ClearWork.Application.Extensions;
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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();