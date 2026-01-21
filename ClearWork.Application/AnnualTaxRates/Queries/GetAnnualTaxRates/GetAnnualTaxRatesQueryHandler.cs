using AutoMapper;
using ClearWork.Application.AnnualTaxRates.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetAnnualTaxRates;

public class GetAnnualTaxRatesQueryHandler
(
    ILogger<GetAnnualTaxRatesQueryHandler> logger,
    IAnnualTaxRateRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetAnnualTaxRatesQuery, IEnumerable<AnnualTaxRateDto>>
{
    public async Task<IEnumerable<AnnualTaxRateDto>> Handle(GetAnnualTaxRatesQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting annual tax rates for user with id [{UserId}]", userId);
        
        var userAnnualTaxRates = await repository.GetUserAnnualTaxRatesAsync(userId);
        
        var userAnnualTaxRatesDto = mapper.Map<IEnumerable<AnnualTaxRateDto>>(userAnnualTaxRates);

        return userAnnualTaxRatesDto;
    }
}