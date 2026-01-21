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

    public async Task<Workplace?> GetUserWorkplaceAsync(string userId, int workplaceId)
    {
        var workplace = await dbContext.Workplaces
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.UserId == userId && w.Id == workplaceId);
        
        return workplace;
    }
}