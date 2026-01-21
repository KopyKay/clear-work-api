using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IWorkplaceRepository
{
    Task<IEnumerable<Workplace>> GetUserWorkplacesAsync(string userId);
    Task<Workplace?> GetUserWorkplaceAsync(string userId, int workplaceId);
}