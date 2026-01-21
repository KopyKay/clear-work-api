using ClearWork.Application.AnnualTaxRates.Dtos;
using MediatR;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetAnnualTaxRate;

public record GetAnnualTaxRateQuery(int Year) : IRequest<AnnualTaxRateDto?>;