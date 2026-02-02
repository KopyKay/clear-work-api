using AutoMapper;
using ClearWork.Application.AnnualTaxRates.Commands.CreateUserAnnualTaxRate;
using ClearWork.Application.AnnualTaxRates.Commands.UpdateUserAnnualTaxRate;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.AnnualTaxRates.Dtos;

public class AnnualTaxRateProfile : Profile
{
    public AnnualTaxRateProfile()
    {
        CreateMap<AnnualTaxRate, AnnualTaxRateDto>();
        
        CreateMap<CreateUserAnnualTaxRateCommand, AnnualTaxRate>();
        
        CreateMap<UpdateUserAnnualTaxRateCommand, AnnualTaxRate>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Year, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
            .ForMember(dest => dest.PensionRate, opt => opt.Condition(src => src.PensionRate.HasValue))
            .ForMember(dest => dest.DisabilityRate, opt => opt.Condition(src => src.DisabilityRate.HasValue))
            .ForMember(dest => dest.SicknessRate, opt => opt.Condition(src => src.SicknessRate.HasValue))
            .ForMember(dest => dest.HealthRate, opt => opt.Condition(src => src.HealthRate.HasValue))
            .ForMember(dest => dest.TaxFreeAmount, opt => opt.Condition(src => src.TaxFreeAmount.HasValue))
            .ForMember(dest => dest.TaxThreshold, opt => opt.Condition(src => src.TaxThreshold.HasValue))
            .ForMember(dest => dest.LowerTaxRate, opt => opt.Condition(src => src.LowerTaxRate.HasValue))
            .ForMember(dest => dest.HigherTaxRate, opt => opt.Condition(src => src.HigherTaxRate.HasValue))
            .ForMember(dest => dest.StandardTaxDeductionMonthly, opt => opt.Condition(src => src.StandardTaxDeductionMonthly.HasValue))
            .ForMember(dest => dest.YoungPersonTaxReliefLimit, opt => opt.Condition(src => src.YoungPersonTaxReliefLimit.HasValue));
    }
}