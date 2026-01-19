using ClearWork.Domain.Enums;

namespace ClearWork.Domain.Entities;

/// <summary>
/// Reprezentuje umowę zawartą między użytkownikiem a pracodawcą.
/// Określa warunki zatrudnienia, typ umowy i stawkę wynagrodzenia.
/// </summary>
public class EmploymentContract
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator miejsca pracy będącego stroną umowy.
    /// </summary>
    public required int WorkplaceId { get; set; }
    
    /// <summary>
    /// Typ umowy użyty do obliczenia.
    /// <list type="bullet">
    ///     <item>Umowa o pracę</item>
    ///     <item>Umowa zlecenie</item>
    ///     <item>Umowa o dzieło</item>
    ///     <item>Działalność gospodarcza</item>
    /// </list>
    /// </summary>
    public required ContractType ContractType { get; set; }
    
    /// <summary>
    /// Wymiar etatu określający proporcję czasu pracy.
    /// <list type="bullet">
    ///     <item>Pełny etat</item>
    ///     <item>3/4 etatu</item>
    ///     <item>1/2 etatu</item>
    ///     <item>1/4 etatu</item>
    /// </list>
    /// </summary>
    public required EmploymentLevel EmploymentLevel { get; set; }
    
    /// <summary>
    /// Stawka godzinowa brutto.
    /// </summary>
    public required decimal HourlyRate { get; set; }
    
    /// <summary>
    /// Dzień miesiąca, w którym następuje wypłata wynagrodzenia.
    /// </summary>
    public int? PaymentDay { get; set; }
    
    /// <summary>
    /// Częstotliwość wypłaty wynagrodzenia.
    /// </summary>
    public required PaymentFrequency PaymentFrequency { get; set; }
    
    /// <summary>
    /// Data i godzina rozpoczęcia obowiązywania umowy.
    /// </summary>
    public required DateTimeOffset StartDateTime { get; set; }
    
    /// <summary>
    /// Data i godzina zakończenia obowiązywania umowy.
    /// Null oznacza umowę na czas nieokreślony.
    /// </summary>
    public DateTimeOffset? EndDateTime { get; set; }
    
    /// <summary>
    /// Data i czas dodania umowy.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Określa, czy umowa jest aktualna.
    /// </summary>
    public bool IsActive { get; set; } = true;

    public Workplace Workplace { get; set; } = null!;
    public ICollection<Paycheck> Paychecks { get; set; } = [];
    public ICollection<TimeEntry> TimeEntries { get; set; } = [];
    public ICollection<BusinessTrip> BusinessTrips { get; set; } = [];
}