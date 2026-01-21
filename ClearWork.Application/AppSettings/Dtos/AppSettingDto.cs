namespace ClearWork.Application.AppSettings.Dtos;

public class AppSettingDto
{
    public bool PushNotificationEnabled { get; set; }
    public TimeOnly? PushNotificationReminderTime { get; set; }
    public bool AppThemeSameAsSystem { get; set; }
    public bool DarkModeEnabled { get; set; }
    public TimeOnly? DarkModeEnableTime { get; set; }
    public TimeOnly? DarkModeDisableTime { get; set; }
}