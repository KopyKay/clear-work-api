using AutoMapper;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.AppSettings.Commands.UpdateUserAppSettings;

public class UpdateUserAppSettingsCommandHandler
(
    ILogger<UpdateUserAppSettingsCommandHandler> logger,
    IAppSettingRepository repository,
    IUserContext userContext,
    IMapper mapper
)
: IRequestHandler<UpdateUserAppSettingsCommand>
{
    public async Task Handle(UpdateUserAppSettingsCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating app settings for user with id [{UserId}]", userId);

        var userAppSettings = await repository.GetUserAppSettingsAsync(userId, true)
            ?? throw new NotFoundException(nameof(AppSetting), string.Empty);
        
        mapper.Map(request, userAppSettings);
        
        await repository.SaveChangesAsync();
    }
}