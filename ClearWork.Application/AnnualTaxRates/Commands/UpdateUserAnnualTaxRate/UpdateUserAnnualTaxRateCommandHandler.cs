using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AnnualTaxRates.Commands.UpdateUserAnnualTaxRate;

public class UpdateUserAnnualTaxRateCommandHandler 
(
    ILogger<UpdateUserAnnualTaxRateCommandHandler> logger,
    IAnnualTaxRateRepository repository,
    IUserContext userContext,
    IMapper mapper    
)    
: IRequestHandler<UpdateUserAnnualTaxRateCommand>
{
    public async Task Handle(UpdateUserAnnualTaxRateCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating annual tax rates for year: {AnnualTaxRateYear} with {@UpdatedAnnualTaxRate}",
            request.Year, request);

        var userAnnualTaxRate = await repository.GetUserAnnualTaxRateAsync(userId, request.Year, true) 
            ?? throw NotFoundException.ForUserResource<AnnualTaxRate>($"year: {request.Year}", userId);
        
        mapper.Map(request, userAnnualTaxRate);

        await repository.SaveChangesAsync();
    }
}