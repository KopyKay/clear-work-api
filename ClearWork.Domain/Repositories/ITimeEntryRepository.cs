using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface ITimeEntryRepository
{
    Task<IEnumerable<TimeEntry>> GetEmploymentContractTimeEntriesAsync(int contractId);
    Task<TimeEntry?> GetEmploymentContractTimeEntryAsync(int contractId, int timeEntryId);
}