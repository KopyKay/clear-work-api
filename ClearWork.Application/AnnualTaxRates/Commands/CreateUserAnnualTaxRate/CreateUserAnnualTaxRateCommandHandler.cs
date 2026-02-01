using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AnnualTaxRates.Commands.CreateUserAnnualTaxRate;

public class CreateUserAnnualTaxRateCommandHandler 
(
    ILogger<CreateUserAnnualTaxRateCommandHandler> logger,
    IAnnualTaxRateRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<CreateUserAnnualTaxRateCommand, int>
{
    public async Task<int> Handle(CreateUserAnnualTaxRateCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("[{UserId}] is creating a new annual taxes rate {@AnnualTaxRate}", userId, request);
        
        var dbUserAnnualTaxRate = await repository.GetUserAnnualTaxRateAsync(userId, request.Year);

        if (dbUserAnnualTaxRate is not null)
        {
            throw new DuplicateResourceException(nameof(AnnualTaxRate), nameof(request.Year), request.Year);
        }
        
        var userAnnualTaxRate = mapper.Map<AnnualTaxRate>(request);
        userAnnualTaxRate.UserId = userId;
        
        var year = await repository.CreateUserAnnualTaxRateAsync(userAnnualTaxRate);
        return year;
    }
}