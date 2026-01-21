using System.Text.Json.Serialization;
using MediatR;

namespace ClearWork.Application.AppSettings.Commands.CreateUserAppSettings;

public record CreateUserAppSettingsCommand : IRequest<int>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public bool PushNotificationEnabled { get; init; }
    public TimeOnly? PushNotificationReminderTime { get; init; }
    public bool AppThemeSameAsSystem { get; init; }
    public bool DarkModeEnabled { get; init; }
    public TimeOnly? DarkModeEnableTime { get; init; }
    public TimeOnly? DarkModeDisableTime { get; init; }
}