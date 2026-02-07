using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class TimeEntryRepository(ClearWorkDbContext dbContext) : ITimeEntryRepository
{
    public async Task<IEnumerable<TimeEntry>> GetEmploymentContractTimeEntriesAsync(int contractId)
    {
        var timeEntries = await dbContext.TimeEntries
            .AsNoTracking()
            .Include(te => te.Calculation)
            .Where(te => te.EmploymentContractId == contractId)
            .ToListAsync();

        return timeEntries;
    }

    public async Task<TimeEntry?> GetEmploymentContractTimeEntryAsync(int contractId, int timeEntryId, bool trackChanges = false)
    {
        var query = dbContext.TimeEntries
            .Include(te => te.Calculation)
            .AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();

        var timeEntry =
            await query.FirstOrDefaultAsync(te => te.EmploymentContractId == contractId && te.Id == timeEntryId);

        return timeEntry;
    }

    public async Task<int> CreateEmploymentContractTimeEntryAsync(TimeEntry entity)
    {
        await dbContext.TimeEntries.AddAsync(entity);
        await SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}