using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IAppSettingRepository
{
    Task<int> CreateUserAppSettingsAsync(AppSetting entity);
    Task<AppSetting> GetUserAppSettingsAsync(string userId);
    Task<AppSetting> GetUserAppSettingsWithTrackingAsync(string userId);
    Task SaveChangesAsync();
}