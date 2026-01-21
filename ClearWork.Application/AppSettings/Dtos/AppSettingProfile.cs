using AutoMapper;
using ClearWork.Application.AppSettings.Commands.CreateUserAppSettings;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.AppSettings.Dtos;

public class AppSettingProfile : Profile
{
    public AppSettingProfile()
    {
        CreateMap<AppSetting, AppSettingDto>();
        CreateMap<CreateUserAppSettingsCommand, AppSetting>();
    }
}