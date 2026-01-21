using AutoMapper;
using ClearWork.Application.AppSettings.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AppSettings.Queries.GetUserAppSettings;

public class GetUserAppSettingsQueryHandler
(
    ILogger<GetUserAppSettingsQueryHandler> logger,
    IAppSettingRepository repository,
    IUserContext userContext,
    IMapper mapper
)
: IRequestHandler<GetUserAppSettingsQuery, AppSettingDto>
{
    public async Task<AppSettingDto> Handle(GetUserAppSettingsQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting app settings for user with id [{UserId}]", userId);
        
        var userAppSettings = await repository.GetUserAppSettingsAsync(userId);
        
        var userAppSettingsDto = mapper.Map<AppSettingDto>(userAppSettings);
        
        return userAppSettingsDto;
    }
}