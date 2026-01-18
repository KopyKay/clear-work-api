using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ClearWork.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ClearWorkDb");
    }
}