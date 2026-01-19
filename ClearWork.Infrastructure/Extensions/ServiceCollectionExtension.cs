using ClearWork.Domain.Entities;
using ClearWork.Infrastructure.Persistence;
using ClearWork.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ClearWork.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ClearWorkDb");

        services.AddDbContext<ClearWorkDbContext>(options =>
            options.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<ClearWorkDbContext>();

        services.AddScoped<IClearWorkSeeder, ClearWorkSeeder>();
    }
}