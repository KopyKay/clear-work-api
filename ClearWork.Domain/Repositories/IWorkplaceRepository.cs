using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IWorkplaceRepository
{
    Task<IEnumerable<Workplace>> GetUserWorkplacesAsync(string userId);
    Task<Workplace?> GetUserWorkplaceAsync(string userId, int workplaceId, bool trackChanges = false);
    Task<int> CreateUserWorkplaceAsync(Workplace entity);
    Task<bool> IsUserWorkplaceNameOccupiedAsync(string userId, string workplaceName);
    Task SaveChangesAsync();
}