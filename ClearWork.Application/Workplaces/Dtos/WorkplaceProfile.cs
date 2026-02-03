using AutoMapper;
using ClearWork.Application.Workplaces.Commands.CreateUserWorkplace;
using ClearWork.Application.Workplaces.Commands.UpdateUserWorkplace;
using ClearWork.Domain.Entities;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.Workplaces.Dtos;

public class WorkplaceProfile : Profile
{
    public WorkplaceProfile()
    {
        CreateMap<Workplace, WorkplaceDto>();

        CreateMap<CreateUserWorkplaceCommand, Workplace>()
            .ForMember(dest => dest.PpkSettings, opt =>
                opt.MapFrom(src => src.PpkSettings ?? new PpkSettingDto 
                {
                    IsActive = false,
                    EmployeeRate = 0,
                    EmployerRate = 0
                }));
        
        CreateMap<UpdateUserWorkplaceCommand, Workplace>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
            .ForMember(dest => dest.Name, opt =>
                opt.Condition(src => !string.IsNullOrWhiteSpace(src.Name)))
            .ForMember(dest => dest.BaseHourlyRate, opt =>
                opt.Condition(src => src.BaseHourlyRate.HasValue))
            .ForMember(dest => dest.PpkSettings, opt =>
                opt.Condition(src => src.PpkSettings != null));


        CreateMap<PpkSetting, PpkSettingDto>().ReverseMap();
        
        CreateMap<UpdatePpkSettingDto, PpkSetting>()
            .ForMember(dest => dest.IsActive, opt =>
                opt.Condition(src => src.IsActive.HasValue))
            .ForMember(dest => dest.EmployeeRate, opt =>
                opt.Condition(src => src.EmployeeRate.HasValue))
            .ForMember(dest => dest.EmployerRate, opt =>
                opt.Condition(src => src.EmployerRate.HasValue))
            .ForMember(dest => dest.IsActive, opt =>
                opt.Condition(src => src.IsActive.HasValue));
    }
}