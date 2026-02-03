using AutoMapper;
using ClearWork.Application.AppSettings.Commands.CreateUserAppSettings;
using ClearWork.Application.AppSettings.Commands.UpdateUserAppSettings;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.AppSettings.Dtos;

public class AppSettingProfile : Profile
{
    public AppSettingProfile()
    {
        CreateMap<AppSetting, AppSettingDto>();
        CreateMap<CreateUserAppSettingsCommand, AppSetting>();
        
        CreateMap<UpdateUserAppSettingsCommand, AppSetting>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PushNotificationEnabled, opt =>
                opt.Condition(src => src.PushNotificationEnabled.HasValue))
            .ForMember(dest => dest.AppThemeSameAsSystem, opt =>
                opt.Condition(src => src.AppThemeSameAsSystem.HasValue))
            .ForMember(dest => dest.DarkModeEnabled, opt =>
                opt.Condition(src => src.DarkModeEnabled.HasValue))
            .ForMember(dest => dest.PushNotificationReminderTime, opt =>
                opt.Condition(src => src.PushNotificationReminderTime.HasValue))
            .ForMember(dest => dest.DarkModeEnableTime, opt =>
                opt.Condition(src => src.DarkModeEnableTime.HasValue))
            .ForMember(dest => dest.DarkModeDisableTime, opt =>
                opt.Condition(src => src.DarkModeDisableTime.HasValue));

    }
}