using ClearWork.Domain.Enums;

namespace ClearWork.Domain.Entities;

/// <summary>
/// Abstrakcyjna klasa bazowa reprezentująca obliczenie wynagrodzenia za wpis czasu pracy/urlopu.
/// Zawiera pełny rozkład składników wynagrodzenia:
/// <list type="bullet">
///     <item>Składki ZUS</item>
///     <item>Zaliczka na PIT</item>
///     <item>Składka zdrowotna</item>
///     <item>PPK</item>
/// </list>
/// </summary>
public abstract class TimeEntryCalculation
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator wpisu czasu, którego dotyczy obliczenie.
    /// </summary>
    public required int TimeEntryId { get; set; }
    
    /// <summary>
    /// Wynagrodzenie brutto przed potrąceniami.
    /// Podstawa do obliczenia składek ZUS i podatku.
    /// </summary>
    public required decimal GrossSalary { get; set; }
    
    /// <summary>
    /// Data i czas wykonania obliczenia.
    /// </summary>
    public DateTimeOffset CalculatedAt { get; set; }
    
    // === Składki ZUS (ubezpieczenia społeczne) ===
    
    /// <summary>
    /// Składka na ubezpieczenie emerytalne.
    /// </summary>
    public required decimal PensionContribution { get; set; }
    
    /// <summary>
    /// Składka na ubezpieczenie rentowe.
    /// </summary>
    public required decimal DisabilityContribution { get; set; }
    
    /// <summary>
    /// Składka na ubezpieczenie chorobowe.
    /// </summary>
    public required decimal SicknessContribution { get; set; }
    
    /// <summary>
    /// Suma wszystkich składek ZUS potrącanych z wynagrodzenia pracownika.
    /// </summary>
    public required decimal TotalZusContributions { get; set; }
    
    // === Podatek dochodowy (PIT) ===
    
    /// <summary>
    /// Koszty uzyskania przychodu.
    /// </summary>
    public required decimal TaxDeduction { get; set; }
    
    /// <summary>
    /// Podstawa opodatkowania.
    /// </summary>
    public required decimal TaxBase { get; set; }
    
    /// <summary>
    /// Zaliczka na podatek dochodowy.
    /// </summary>
    public required decimal TaxAmount { get; set; }
    
    // === Składka zdrowotna i PPK ===
    
    /// <summary>
    /// Składka na ubezpieczenie zdrowotne.
    /// </summary>
    public required decimal HealthContribution { get; set; }
    
    /// <summary>
    /// Składka PPK finansowana przez pracownika.
    /// </summary>
    public required decimal PpkEmployeeContribution { get; set; }
    
    /// <summary>
    /// Składka PPK finansowana przez pracodawcę.
    /// </summary>
    public required decimal PpkEmployerContribution { get; set; }
    
    /// === Wynik końcowy ===
    /// 
    /// <summary>
    /// Wynagrodzenie netto.
    /// </summary>
    public required decimal NetSalary { get; set; }
    
    // === Metadata ===
    
    /// <summary>
    /// Określa, czy zastosowano ulgę dla młodych (zerowy PIT).
    /// </summary>
    public bool YoungPersonReliefApplied { get; set; } = false;
    
    /// <summary>
    /// Określa, czy zastosowano zwolnienie z ZUS dla studenta.
    /// </summary>
    public bool StudentExemptionApplied { get; set; } = false;
    
    /// <summary>
    /// Typ zastosowanych kosztów uzyskania przychodu.
    /// <list type="bullet">
    ///     <item>Brak</item>
    ///     <item>Standardowe</item>
    ///     <item>Praca twórcza</item>
    ///     <item>Procentowe</item>
    ///     <item>Koszty rzeczywiste</item>
    ///     <item>Inne</item>
    /// </list>
    /// </summary>
    public TaxDeductionType? AppliedTaxDeductionType { get; set; }
    
    /// <summary>
    /// Typ umowy użyty do obliczenia.
    /// <list type="bullet">
    ///     <item>Umowa o pracę</item>
    ///     <item>Umowa zlecenie</item>
    ///     <item>Umowa o dzieło</item>
    ///     <item>Działalność gospodarcza</item>
    /// </list>
    /// </summary>
    public required ContractType ContractTypeUsed { get; set; }
    
    public TimeEntry TimeEntry { get; set; } = null!;
}

/// <summary>
/// Obliczenie wynagrodzenia specyficzne dla wpisu czasu pracy.
/// Zawiera informacje o przepracowanych godzinach, typie pracy i zastosowanych mnożnikach.
/// </summary>
public class WorkEntryCalculation : TimeEntryCalculation
{
    /// <summary>
    /// Całkowita liczba przepracowanych godzin.
    /// </summary>
    public required decimal TotalHours { get; set; }
    
    /// <summary>
    /// Typ pracy określający mnożnik wynagrodzenia zgodnie z Kodeksem pracy:
    /// <list type="bullet">
    ///     <item>Standardowe godziny</item>
    ///     <item>Nadgodziny</item>
    ///     <item>Praca w nocy</item>
    ///     <item>Niedziela/święto</item>
    ///     <item>Sobota</item>
    /// </list>
    /// </summary>
    public required ShiftType ShiftType { get; set; }
    
    /// <summary>
    /// Mnożnik wynagrodzenia zastosowany dla danego typu pracy.
    /// </summary>
    public required decimal Multiplier { get; set; }
    
    /// <summary>
    /// Stawka godzinowa brutto użyta do obliczeń.
    /// </summary>
    public required decimal HourlyRateUsed { get; set; }
}

/// <summary>
/// Obliczenie wynagrodzenia specyficzne dla wpisu urlopu.
/// Zawiera informacje o dniach roboczych, stawce dziennej i źródle finansowania.
/// </summary>
public class LeaveEntryCalculation : TimeEntryCalculation
{
    /// <summary>
    /// Liczba dni roboczych objętych urlopem.
    /// </summary>
    public required int WorkingDays { get; set; }
    
    /// <summary>
    /// Stawka dzienna za dzień urlopu, obliczana jako średnia z okresu referencyjnego.
    /// </summary>
    public required decimal DailyRate { get; set; }
    
    /// <summary>
    /// Określa, czy wynagrodzenie jest finansowane przez ZUS.
    /// <list type="bullet">
    ///     <item>True — zasiłek ZUS</item>
    ///     <item>False — wynagrodzenie chorobowe od pracodawcy</item>
    /// </list>
    /// </summary>
    public bool IsPaidBySocialSecurity { get; set; } = false;
}