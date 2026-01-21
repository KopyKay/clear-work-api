using ClearWork.Application.AnnualTaxRates.Dtos;
using MediatR;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetAnnualTaxRates;

public record GetAnnualTaxRatesQuery : IRequest<IEnumerable<AnnualTaxRateDto>>;