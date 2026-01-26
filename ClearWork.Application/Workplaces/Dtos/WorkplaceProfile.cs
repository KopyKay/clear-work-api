using AutoMapper;
using ClearWork.Domain.Entities;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.Workplaces.Dtos;

public class WorkplaceProfile : Profile
{
    public WorkplaceProfile()
    {
        CreateMap<Workplace, WorkplaceDto>();
        
        CreateMap<PpkSetting, PpkSettingDto>();
    }
}