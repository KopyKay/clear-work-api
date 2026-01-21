using AutoMapper;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.AnnualTaxRates.Dtos;

public class AnnualTaxRateProfile : Profile
{
    public AnnualTaxRateProfile()
    {
        CreateMap<AnnualTaxRate, AnnualTaxRateDto>();
    }
}