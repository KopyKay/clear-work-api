using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class WorkplaceRepository(ClearWorkDbContext dbContext) : IWorkplaceRepository
{
    public async Task<IEnumerable<Workplace>> GetUserWorkplacesAsync(string userId)
    {
        var workplaces = await dbContext.Workplaces
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .ToListAsync();

        return workplaces;
    }

    public async Task<Workplace?> GetUserWorkplaceAsync(string userId, int workplaceId, bool trackChanges = false)
    {
        var query = dbContext.Workplaces.AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();

        var workplace = await query.FirstOrDefaultAsync(w => w.UserId == userId && w.Id == workplaceId);
        
        return workplace;
    }

    public async Task<int> CreateUserWorkplaceAsync(Workplace entity)
    {
        await dbContext.Workplaces.AddAsync(entity);
        await SaveChangesAsync();

        return entity.Id;
    }

    public Task<bool> IsUserWorkplaceNameOccupiedAsync(string userId, string workplaceName)
    {
        return dbContext.Workplaces
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .AnyAsync(w => w.Name == workplaceName);
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}