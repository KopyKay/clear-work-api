using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class BusinessTripRepository(ClearWorkDbContext dbContext) : IBusinessTripRepository
{
    public async Task<IEnumerable<BusinessTrip>> GetEmploymentContractBusinessTripsAsync(int contractId)
    {
        var businessTrips = await dbContext.BusinessTrips
            .AsNoTracking()
            .Where(bt => bt.EmploymentContractId == contractId)
            .ToListAsync();
        
        return businessTrips;
    }

    public async Task<BusinessTrip?> GetEmploymentContractBusinessTripAsync(int contractId, int businessTripId)
    {
        var businessTrip = await dbContext.BusinessTrips
            .AsNoTracking()
            .FirstOrDefaultAsync(bt => bt.EmploymentContractId == contractId && bt.Id == businessTripId);

        return businessTrip;
    }
}