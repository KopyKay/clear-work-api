using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IEmploymentContractRepository
{
    Task<IEnumerable<EmploymentContract>> GetWorkplaceEmploymentContractsAsync(int workplaceId);
    Task<EmploymentContract?> GetWorkplaceEmploymentContractAsync(int workplaceId, int contractId, bool trackChanges = false);
    Task<int> CreateWorkplaceEmploymentContractAsync(EmploymentContract entity);
    Task SaveChangesAsync();
}