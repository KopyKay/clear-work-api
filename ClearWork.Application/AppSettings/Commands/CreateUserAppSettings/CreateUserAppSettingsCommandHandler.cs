using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AppSettings.Commands.CreateUserAppSettings;

public class CreateUserAppSettingsCommandHandler
(
    ILogger<CreateUserAppSettingsCommandHandler> logger,
    IAppSettingRepository repository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<CreateUserAppSettingsCommand, int>
{
    public async Task<int> Handle(CreateUserAppSettingsCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Creating app settings for user with id [{UserId}]", userId);
        
        request.UserId = userId;
        var userAppSettings = mapper.Map<AppSetting>(request);
        var userAppSettingsId = await repository.CreateUserAppSettingsAsync(userAppSettings);

        return userAppSettingsId;
    }
}