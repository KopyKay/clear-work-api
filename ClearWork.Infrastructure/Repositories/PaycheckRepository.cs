using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class PaycheckRepository(ClearWorkDbContext dbContext) : IPaycheckRepository
{
    public async Task<IEnumerable<Paycheck>> GetEmploymentContractPaychecksAsync(int contractId)
    {
        var paychecks = await dbContext.Paychecks
            .AsNoTracking()
            .Where(p => p.EmploymentContractId == contractId)
            .ToListAsync();
        
        return paychecks;
    }

    public async Task<Paycheck?> GetEmploymentContractPaycheckAsync(int contractId, int paycheckId, bool trackChanges = false)
    {
        var query = dbContext.Paychecks.AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();
        
        var paycheck = await query.FirstOrDefaultAsync(p => p.EmploymentContractId == contractId && p.Id == paycheckId);

        return paycheck;
    }

    public async Task<int> CreateEmploymentContractPaycheckAsync(Paycheck entity)
    {
        await dbContext.Paychecks.AddAsync(entity);
        await SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}