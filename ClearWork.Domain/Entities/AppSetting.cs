namespace ClearWork.Domain.Entities;

/// <summary>
/// Reprezentuje ustawienia aplikacji specyficzne dla użytkownika.
/// Obejmuje preferencje powiadomień i motywu wizualnego.
/// </summary>
public class AppSetting
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator użytkownika, którego dotyczą ustawienia.
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Określa, czy powiadomienia push są włączone.
    /// </summary>
    public bool PushNotificationEnabled { get; set; } = false;
    
    /// <summary>
    /// Godzina wysyłania dziennego powiadomienia push.
    /// </summary>
    public TimeOnly? PushNotificationReminderTime { get; set; }
    
    /// <summary>
    /// Określa, czy motyw aplikacji ma być synchronizowany z ustawieniami systemu.
    /// </summary>
    public bool AppThemeSameAsSystem { get; set; } = true;
    
    /// <summary>
    /// Określa, czy tryb ciemny jest włączony (gdy <b>AppThemeSameAsSystem</b> = false).
    /// <list type="bullet">
    ///     <item>True — ciemny motyw</item>
    ///     <item>False — jasny motyw</item>
    /// </list>
    /// </summary>
    public bool DarkModeEnabled { get; set; } = false;
    
    /// <summary>
    /// Godzina automatycznego włączenia trybu ciemnego.
    /// </summary>
    public TimeOnly? DarkModeEnableTime { get; set; }
    
    /// <summary>
    /// Godzina automatycznego wyłączenia trybu ciemnego.
    /// </summary>
    public TimeOnly? DarkModeDisableTime { get; set; }

    public User User { get; set; } = null!;
}