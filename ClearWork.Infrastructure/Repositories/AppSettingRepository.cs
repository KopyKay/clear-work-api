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

    public async Task<AppSetting> GetUserAppSettingsAsync(string userId)
    {
        var appSetting = await dbContext.AppSettings
            .AsNoTracking()
            .FirstAsync(@as => @as.UserId == userId);

        return appSetting;
    }

    public async Task<AppSetting> GetUserAppSettingsWithTrackingAsync(string userId)
    {
        var appSetting = await dbContext.AppSettings
            .FirstAsync(@as => @as.UserId == userId);

        return appSetting;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}