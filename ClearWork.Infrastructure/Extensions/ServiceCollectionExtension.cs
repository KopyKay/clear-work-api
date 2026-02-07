using ClearWork.Application.TimeEntries.Services;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using ClearWork.Infrastructure.Repositories;
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
        services.AddScoped<IAnnualTaxRateRepository, AnnualTaxRateRepository>();
        services.AddScoped<IAppSettingRepository, AppSettingRepository>();
        services.AddScoped<IBusinessTripRepository, BusinessTripRepository>();
        services.AddScoped<IEmploymentContractRepository, EmploymentContractRepository>();
        services.AddScoped<IPaycheckRepository, PaycheckRepository>();
        services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
        services.AddScoped<IWorkplaceRepository, WorkplaceRepository>();
        
        services.AddScoped<ISalaryCalculationService, SalaryCalculationService>();
    }
}