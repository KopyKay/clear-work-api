using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class AnnualTaxRateRepository(ClearWorkDbContext dbContext) : IAnnualTaxRateRepository
{
    public async Task<IEnumerable<AnnualTaxRate>> GetUserAnnualTaxRatesAsync(string userId)
    {
        var annualTaxRates = await dbContext.AnnualTaxRates
            .AsNoTracking()
            .Where(atr => atr.UserId == userId)
            .ToListAsync();

        return annualTaxRates;
    }

    public async Task<AnnualTaxRate?> GetUserAnnualTaxRateAsync(string userId, int year, bool trackChanges = false)
    {
        var query = dbContext.AnnualTaxRates.AsQueryable();

        if (!trackChanges) 
            query = query.AsNoTracking();
        
        var annualTaxRate = await query.FirstOrDefaultAsync(atr => atr.UserId == userId && atr.Year == year);
        
        return annualTaxRate;
    }

    public async Task<int> CreateUserAnnualTaxRateAsync(AnnualTaxRate entity)
    {
        await dbContext.AddAsync(entity);
        await SaveChangesAsync();

        return entity.Year;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}