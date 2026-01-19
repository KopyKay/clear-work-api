using ClearWork.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Persistence;

internal class ClearWorkDbContext(DbContextOptions<ClearWorkDbContext> options) : IdentityUserContext<User>(options)
{
    internal DbSet<AppSetting> AppSettings { get; set; }
    internal DbSet<AnnualTaxRate> AnnualTaxRates { get; set; }
    internal DbSet<Workplace> Workplaces { get; set; }
    internal DbSet<EmploymentContract> EmploymentContracts { get; set; }
    internal DbSet<Paycheck> Paychecks { get; set; }
    internal DbSet<BusinessTrip> BusinessTrips { get; set; }
    internal DbSet<TimeEntry> TimeEntries { get; set; }
    internal DbSet<TimeEntryCalculation> TimeEntryCalculations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.ApplyConfigurationsFromAssembly(typeof(ClearWorkDbContext).Assembly);
    }
}