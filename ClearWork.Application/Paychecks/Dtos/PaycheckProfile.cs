using AutoMapper;
using ClearWork.Application.Paychecks.Commands.CreateEmploymentContractPaycheck;
using ClearWork.Application.Paychecks.Commands.UpdateEmploymentContractPaycheck;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.Paychecks.Dtos;

public class PaycheckProfile : Profile
{
    public PaycheckProfile()
    {
        CreateMap<Paycheck, PaycheckDto>();

        CreateMap<CreateEmploymentContractPaycheckCommand, Paycheck>();

        CreateMap<UpdateEmploymentContractPaycheckCommand, Paycheck>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EmploymentContractId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.PaymentDate, opt =>
                opt.Condition(src => src.PaymentDate.HasValue))
            .ForMember(dest => dest.GrossAmount, opt =>
                opt.Condition(src => src.GrossAmount.HasValue))
            .ForMember(dest => dest.NetAmount, opt =>
                opt.Condition(src => src.NetAmount.HasValue))
            .ForMember(dest => dest.ModifiedAt, opt =>
                opt.MapFrom(_ => DateTimeOffset.UtcNow));
    }
}