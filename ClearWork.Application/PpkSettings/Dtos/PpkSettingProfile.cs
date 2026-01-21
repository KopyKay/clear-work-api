using AutoMapper;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.PpkSettings.Dtos;

public class PpkSettingProfile : Profile
{
    public PpkSettingProfile()
    {
        CreateMap<PpkSetting, PpkSettingDto>();
    }
}