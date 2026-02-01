using ClearWork.Application.AnnualTaxRates.Dtos;
using MediatR;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetUserAnnualTaxRates;

public record GetUserAnnualTaxRatesQuery : IRequest<IEnumerable<AnnualTaxRateDto>>;