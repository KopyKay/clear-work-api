using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Workplaces.Commands.CreateUserWorkplace;

public class CreateUserWorkplaceCommandHandler 
(
    ILogger<CreateUserWorkplaceCommandHandler> logger,
    IWorkplaceRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<CreateUserWorkplaceCommand, int>
{
    public async Task<int> Handle(CreateUserWorkplaceCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("[{UserId}] is creating a new workplace {@Workplace}", userId, request);

        var isUserWorkplaceNameOccupied = await repository.IsUserWorkplaceNameOccupiedAsync(userId, request.Name);

        if (isUserWorkplaceNameOccupied)
        {
            throw new DuplicateResourceException(nameof(Workplace), nameof(request.Name), request.Name);
        }
        
        var userWorkplace = mapper.Map<Workplace>(request);
        userWorkplace.UserId = userId;
        
        var workplaceId = await repository.CreateUserWorkplaceAsync(userWorkplace);
        return workplaceId;
    }
}