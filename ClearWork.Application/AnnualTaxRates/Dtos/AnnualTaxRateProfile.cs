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
            .ForMember(dest => dest.Year, opt => opt.Ignore());
    }
}