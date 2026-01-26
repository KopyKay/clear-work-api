using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Application.Workplaces.Dtos;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Workplaces.Queries.GetUserWorkplace;

public class GetUserWorkplaceQueryHandler 
(
    ILogger<GetUserWorkplaceQueryHandler> logger,    
    IWorkplaceRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetUserWorkplaceQuery, WorkplaceDto?>
{
    public async Task<WorkplaceDto?> Handle(GetUserWorkplaceQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting workplace with id [{WorkplaceId}] for user with id [{UserId}]", request.WorkplaceId, userId);

        var userWorkplace = await repository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        var userWorkplaceDto = mapper.Map<WorkplaceDto>(userWorkplace);

        return userWorkplaceDto;
    }
}