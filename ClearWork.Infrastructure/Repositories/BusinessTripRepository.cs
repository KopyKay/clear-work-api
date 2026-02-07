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

    public async Task<BusinessTrip?> GetEmploymentContractBusinessTripAsync(int contractId, int businessTripId, bool trackChanges = false)
    {
        var query = dbContext.BusinessTrips.AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();
        
        var businessTrip = await query
            .FirstOrDefaultAsync(bt => bt.EmploymentContractId == contractId && bt.Id == businessTripId);

        return businessTrip;
    }

    public async Task<int> CreateEmploymentContractBusinessTripAsync(BusinessTrip entity)
    {
        await dbContext.BusinessTrips.AddAsync(entity);
        await SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}