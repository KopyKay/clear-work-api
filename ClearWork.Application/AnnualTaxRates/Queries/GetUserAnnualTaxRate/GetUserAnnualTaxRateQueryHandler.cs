using AutoMapper;
using ClearWork.Application.AnnualTaxRates.Dtos;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetUserAnnualTaxRate;

public class GetUserAnnualTaxRateQueryHandler 
(
    ILogger<GetUserAnnualTaxRateQueryHandler> logger,
    IAnnualTaxRateRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetUserAnnualTaxRateQuery, AnnualTaxRateDto?>
{
    public async Task<AnnualTaxRateDto?> Handle(GetUserAnnualTaxRateQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting annual tax rate from year [{Year}] for user with id [{UserId}]", request.Year, userId);

        var userAnnualTaxRate = await repository.GetUserAnnualTaxRateOrThrowAsync(userId, request.Year);
        
        var userAnnualTaxRateDto = mapper.Map<AnnualTaxRateDto>(userAnnualTaxRate);
        
        return userAnnualTaxRateDto;
    }
}