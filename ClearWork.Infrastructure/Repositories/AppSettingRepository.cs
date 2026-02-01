using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using ClearWork.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClearWork.Infrastructure.Repositories;

internal class AppSettingRepository(ClearWorkDbContext dbContext) : IAppSettingRepository
{
    public async Task<int> CreateUserAppSettingsAsync(AppSetting entity)
    {
        await dbContext.AppSettings.AddAsync(entity);
        await SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task<AppSetting?> GetUserAppSettingsAsync(string userId, bool trackChanges = false)
    {
        var query = dbContext.AppSettings.AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();
        
        var appSetting = await query.FirstOrDefaultAsync(@as => @as.UserId == userId);

        return appSetting;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}