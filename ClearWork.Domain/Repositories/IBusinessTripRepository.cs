using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IBusinessTripRepository
{
    Task<IEnumerable<BusinessTrip>> GetEmploymentContractBusinessTripsAsync(int contractId);
    Task<BusinessTrip?> GetEmploymentContractBusinessTripAsync(int contractId, int businessTripId, bool trackChanges = false);
    Task<int> CreateEmploymentContractBusinessTripAsync(BusinessTrip entity);
    Task SaveChangesAsync();
}