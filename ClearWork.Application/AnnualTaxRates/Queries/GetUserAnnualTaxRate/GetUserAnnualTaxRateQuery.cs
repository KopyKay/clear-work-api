using ClearWork.Application.AnnualTaxRates.Dtos;
using MediatR;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetUserAnnualTaxRate;

public record GetUserAnnualTaxRateQuery(int Year) : IRequest<AnnualTaxRateDto?>;