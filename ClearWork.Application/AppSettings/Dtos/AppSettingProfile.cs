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
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}