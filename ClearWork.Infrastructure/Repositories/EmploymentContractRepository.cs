using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class EmploymentContractRepository(ClearWorkDbContext dbContext) : IEmploymentContractRepository
{
    public async Task<IEnumerable<EmploymentContract>> GetWorkplaceEmploymentContractsAsync(int workplaceId)
    {
        var employmentContracts = await dbContext.EmploymentContracts
            .AsNoTracking()
            .Where(ec => ec.WorkplaceId == workplaceId)
            .ToListAsync();

        return employmentContracts;
    }

    public async Task<EmploymentContract?> GetWorkplaceEmploymentContractAsync(int workplaceId, int contractId)
    {
        var employmentContract = await dbContext.EmploymentContracts
            .AsNoTracking()
            .FirstOrDefaultAsync(ec => ec.WorkplaceId == workplaceId && ec.Id == contractId);

        return employmentContract;
    }
}