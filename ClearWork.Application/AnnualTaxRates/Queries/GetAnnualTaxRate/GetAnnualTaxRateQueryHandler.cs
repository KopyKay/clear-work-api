using AutoMapper;
using ClearWork.Application.AnnualTaxRates.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AnnualTaxRates.Queries.GetAnnualTaxRate;

public class GetAnnualTaxRateQueryHandler 
(
    ILogger<GetAnnualTaxRateQueryHandler> logger,
    IAnnualTaxRateRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetAnnualTaxRateQuery, AnnualTaxRateDto?>
{
    public async Task<AnnualTaxRateDto?> Handle(GetAnnualTaxRateQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting annual tax rate from year [{Year}] for user with id [{UserId}]", request.Year, userId);
        
        var userAnnualTaxRate = await repository.GetUserAnnualTaxRateAsync(userId, request.Year)
            ?? throw new NotFoundException(nameof(AnnualTaxRate),
                $"{request.Year.ToString()} year for this user");
        
        var userAnnualTaxRateDto = mapper.Map<AnnualTaxRateDto>(userAnnualTaxRate);
        
        return userAnnualTaxRateDto;
    }
}