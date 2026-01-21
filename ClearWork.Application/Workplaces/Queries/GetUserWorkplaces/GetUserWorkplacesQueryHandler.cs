using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Application.Workplaces.Dtos;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Workplaces.Queries.GetUserWorkplaces;

public class GetUserWorkplacesQueryHandler
(
    ILogger<GetUserWorkplacesQueryHandler> logger,
    IWorkplaceRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetUserWorkplacesQuery, IEnumerable<WorkplaceDto>>
{
    public async Task<IEnumerable<WorkplaceDto>> Handle(GetUserWorkplacesQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting workplaces for user with id [{UserId}]", userId);
        
        var userWorkplaces = await repository.GetUserWorkplacesAsync(userId);
        
        var userWorkplacesDto = mapper.Map<IEnumerable<WorkplaceDto>>(userWorkplaces);
        
        return userWorkplacesDto;
    }
}