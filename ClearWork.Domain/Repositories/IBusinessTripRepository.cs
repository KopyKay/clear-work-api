using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IBusinessTripRepository
{
    Task<IEnumerable<BusinessTrip>> GetEmploymentContractBusinessTripsAsync(int contractId);
    Task<BusinessTrip?> GetEmploymentContractBusinessTripAsync(int contractId, int businessTripId);
}