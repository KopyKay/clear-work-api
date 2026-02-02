using AutoMapper;
using ClearWork.Application.EmploymentContracts.Commands.CreateWorkplaceEmploymentContract;
using ClearWork.Application.EmploymentContracts.Commands.UpdateWorkplaceEmploymentContract;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.EmploymentContracts.Dtos;

public class EmploymentContractProfile : Profile
{
    public EmploymentContractProfile()
    {
        CreateMap<EmploymentContract, EmploymentContractDto>();

        CreateMap<CreateWorkplaceEmploymentContractCommand, EmploymentContract>()
            .ForMember(dest => dest.StartDateTime, opt =>
                opt.MapFrom(src => src.StartDateTime.ToUniversalTime()))
            .ForMember(dest => dest.EndDateTime, opt =>
                opt.MapFrom(src => src.EndDateTime.HasValue
                    ? src.EndDateTime.Value.ToUniversalTime()
                    : (DateTimeOffset?)null));
        
        CreateMap<UpdateWorkplaceEmploymentContractCommand, EmploymentContract>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.WorkplaceId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
            .ForMember(dest => dest.ContractType, opt =>
                opt.Condition(src => src.ContractType.HasValue))
            .ForMember(dest => dest.EmploymentLevel, opt =>
                opt.Condition(src => src.EmploymentLevel.HasValue))
            .ForMember(dest => dest.HourlyRate, opt =>
                opt.Condition(src => src.HourlyRate.HasValue))
            .ForMember(dest => dest.PaymentDay, opt =>
                opt.Condition(src => src.PaymentDay.HasValue))
            .ForMember(dest => dest.PaymentFrequency, opt =>
                opt.Condition(src => src.PaymentFrequency.HasValue))
            .ForMember(dest => dest.StartDateTime, opt =>
            {
                opt.PreCondition(src => src.StartDateTime.HasValue);
                opt.MapFrom(src => src.StartDateTime!.Value.ToUniversalTime());
            })
            .ForMember(dest => dest.EndDateTime, opt =>
                opt.MapFrom(src => src.EndDateTime.HasValue
                    ? src.EndDateTime.Value.ToUniversalTime()
                    : (DateTimeOffset?)null))
            .ForMember(dest => dest.IsActive, opt =>
                opt.Condition(src => src.IsActive.HasValue));
    }
}