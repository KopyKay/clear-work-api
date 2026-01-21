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

    public async Task<AnnualTaxRate?> GetUserAnnualTaxRateAsync(string userId, int year)
    {
        var annualTaxRate = await dbContext.AnnualTaxRates
            .AsNoTracking()
            .FirstOrDefaultAsync(atr => atr.UserId == userId && atr.Year == year);
        
        return annualTaxRate;
    }
}