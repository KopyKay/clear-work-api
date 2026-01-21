using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IAnnualTaxRateRepository
{
    Task<IEnumerable<AnnualTaxRate>> GetUserAnnualTaxRatesAsync(string userId);
    Task<AnnualTaxRate?> GetUserAnnualTaxRateAsync(string userId, int year);
}