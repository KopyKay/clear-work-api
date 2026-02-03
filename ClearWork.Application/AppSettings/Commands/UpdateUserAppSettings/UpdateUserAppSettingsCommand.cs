using MediatR;

namespace ClearWork.Application.AppSettings.Commands.UpdateUserAppSettings;

public record UpdateUserAppSettingsCommand : IRequest
{
    public bool? PushNotificationEnabled { get; init; }
    public TimeOnly? PushNotificationReminderTime { get; init; }
    public bool? AppThemeSameAsSystem { get; init; }
    public bool? DarkModeEnabled { get; init; }
    public TimeOnly? DarkModeEnableTime { get; init; }
    public TimeOnly? DarkModeDisableTime { get; init; }
}