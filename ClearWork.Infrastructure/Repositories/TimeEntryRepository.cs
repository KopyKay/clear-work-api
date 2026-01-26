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

    public async Task<TimeEntry?> GetEmploymentContractTimeEntryAsync(int contractId, int timeEntryId)
    {
        var timeEntry = await dbContext.TimeEntries
            .AsNoTracking()
            .Include(te => te.Calculation)
            .FirstOrDefaultAsync(te => te.EmploymentContractId == contractId && te.Id == timeEntryId);

        return timeEntry;
    }
}