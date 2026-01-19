using ClearWork.Domain.Enums;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Domain.Entities;

/// <summary>
/// Abstrakcyjna klasa bazowa reprezentująca wpis czasu pracy (WorkEntry)/urlopu (LeaveEntry).
/// Służy do rejestrowania i śledzenia aktywności pracowniczej.
/// </summary>
public abstract class TimeEntry
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator umowy w danym miejscu pracy.
    /// </summary>
    public required int EmploymentContractId { get; set; }
    
    /// <summary>
    /// Data i godzina rozpoczęcia pracy/urlopu.
    /// </summary>
    public required DateTimeOffset StartDateTime { get; set; }
    
    /// <summary>
    /// Data i godzina zakończenia pracy/urlopu.
    /// </summary>
    public required DateTimeOffset EndDateTime { get; set; }
    
    /// <summary>
    /// Opcjonalna notatka tekstowa do wpisu.
    /// </summary>
    public string? TextNote { get; set; }
    
    /// <summary>
    /// Data i czas utworzenia wpisu.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Data i czas ostatniej modyfikacji wpisu.
    /// </summary>
    public DateTimeOffset? ModifiedAt { get; set; }
    
    /// <summary>
    /// Opcjonalna notatka głosowa dołączona do wpisu.
    /// </summary>
    public AudioNote? AudioNote { get; set; }

    public EmploymentContract EmploymentContract { get; set; } = null!;
    public TimeEntryCalculation Calculation { get; set; } = null!;
}

/// <summary>
/// Reprezentuje wpis czasu pracy — zarejestrowane godziny przepracowane przez użytkownika.
/// Może być powiązany z podróżą służbową i zawierać szczegóły dotyczące danego dnia delegacji.
/// </summary>
public class WorkEntry : TimeEntry
{
    /// <summary>
    /// Identyfikator delegacji, w ramach której wykonywana była praca.
    /// </summary>
    public int? BusinessTripId { get; set; }
    
    /// <summary>
    /// Szczegóły dnia delegacji — informacje o zapewnionych posiłkach i noclegu.
    /// </summary>
    public DailyBusinessTripDetail? DailyBusinessTripDetail { get; set; }

    public BusinessTrip BusinessTrip { get; set; } = null!;
}

/// <summary>
/// Reprezentuje wpis nieobecności — urlop, zwolnienie lekarskie lub inne dni wolne.
/// </summary>
public class LeaveEntry : TimeEntry
{
    /// <summary>
    /// Typ urlopu określający sposób naliczania wynagrodzenia.
    /// <list type="bullet">
    ///     <item>Wypoczynkowy</item>
    ///     <item>Zwolnienie lekarskie</item>
    ///     <item>Okolicznościowy</item>
    /// </list>
    /// </summary>
    public required LeaveType LeaveType { get; set; }
    
    /// <summary>
    /// Liczba dni roboczych objętych urlopem.
    /// </summary>
    public required int WorkingDays { get; set; }
}