using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Workplaces.Commands.UpdateUserWorkplace;

public class UpdateUserWorkplaceCommandHandler 
(
    ILogger<UpdateUserWorkplaceCommandHandler> logger,
    IWorkplaceRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<UpdateUserWorkplaceCommand>
{
    public async Task Handle(UpdateUserWorkplaceCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating {Name} workplace with {@UpdatedAnnualTaxRate}",
            request.Name, request);

        var userWorkplace = await repository.GetUserWorkplaceAsync(userId, request.Id, true)
            ?? throw NotFoundException.ForUserResource<Workplace>(request.Id, userId);

        if (!string.IsNullOrWhiteSpace(request.Name) && request.Name != userWorkplace.Name)
        {
            var isUserWorkplaceNameOccupied = await repository.IsUserWorkplaceNameOccupiedAsync(userId, request.Name);
        
            if (isUserWorkplaceNameOccupied)
                throw new DuplicateResourceException(nameof(Workplace), nameof(request.Name), request.Name);
        }
        
        mapper.Map(request, userWorkplace);

        await repository.SaveChangesAsync();
    }
}