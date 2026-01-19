using ClearWork.Domain.Constants;

namespace ClearWork.Domain.Entities;

/// <summary>
/// Reprezentuje stawki podatkowe i składkowe dla danego roku podatkowego.
/// Umożliwia użytkownikowi nadpisanie domyślnych wartości obowiązujących w Polsce.
/// Przydatne przy zmianach przepisów lub specyficznych sytuacjach podatkowych.
/// </summary>
public class AnnualTaxRate
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator użytkownika, którego dotyczą te stawki.
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Rok podatkowy, którego dotyczą te stawki.
    /// </summary>
    public required int Year { get; set; }
    
    // === Składki ZUS (ubezpieczenia społeczne) ===
    
    /// <summary>
    /// Stawka składki emerytalnej (część pracownika) jako ułamek dziesiętny.
    /// Wartość domyślna: 0.0976 (9,76%).
    /// </summary>
    public decimal PensionRate { get; set; } = TaxRates.PensionRateDefaultValue;
    
    /// <summary>
    /// Stawka składki rentowej (część pracownika) jako ułamek dziesiętny.
    /// Wartość domyślna: 0.015 (1,5%).
    /// </summary>
    public decimal DisabilityRate { get; set; } = TaxRates.DisabilityRateDefaultValue;
    
    /// <summary>
    /// Stawka składki chorobowej jako ułamek dziesiętny.
    /// Wartość domyślna: 0.0245 (2,45%).
    /// </summary>
    public decimal SicknessRate { get; set; } = TaxRates.SicknessRateDefaultValue;
    
    /// <summary>
    /// Stawka składki zdrowotnej jako ułamek dziesiętny.
    /// Wartość domyślna: 0.09 (9%) zgodnie z ustawą o świadczeniach zdrowotnych.
    /// </summary>
    public decimal HealthRate { get; set; } = TaxRates.HealthRateDefaultValue;
    
    // === Podatek dochodowy (PIT) ===
    
    /// <summary>
    /// Roczna kwota wolna od podatku.
    /// Wartość domyślna: 30 000 PLN.
    /// </summary>
    public decimal TaxFreeAmount { get; set; } = TaxRates.TaxFreeAmountDefaultValue;
    
    /// <summary>
    /// Roczny próg podatkowy.
    /// Wartość domyślna: 120 000 PLN.
    /// Dochód do progu: 12% PIT, powyżej progu: 32% PIT.
    /// </summary>
    public decimal TaxThreshold { get; set; } = TaxRates.TaxThresholdDefaultValue;
    
    /// <summary>
    /// Niższa stawka podatku PIT (I próg) jako ułamek dziesiętny.
    /// Wartość domyślna: 0.12 (12%).
    /// </summary>
    public decimal LowerTaxRate { get; set; } = TaxRates.LowerTaxRateDefaultValue;
    
    /// <summary>
    /// Wyższa stawka podatku PIT (II próg) jako ułamek dziesiętny.
    /// Wartość domyślna: 0.32 (32%) - dla dochodu powyżej 120 000 PLN rocznie.
    /// </summary>
    public decimal HigherTaxRate { get; set; } = TaxRates.HigherTaxRateDefaultValue;
    
    // === Koszty uzyskania przychodu (KUP) ===
    
    /// <summary>
    /// Standardowe miesięczne koszty uzyskania przychodu.
    /// Wartość domyślna: 250 PLN (pracownik miejscowy) lub 300 PLN (zamiejscowy).
    /// Dla umów autorskich: 50% przychodu..
    /// </summary>
    public decimal StandardTaxDeductionMonthly { get; set; } = TaxRates.StandardTaxDeductionMonthlyDefaultValue;
    
    /// <summary>
    /// Roczny limit ulgi dla młodych (zerowy PIT do 26 r.ż.).
    /// Wartość domyślna: 85 528 PLN.
    /// Po przekroczeniu limitu stosuje się standardowe opodatkowanie.
    /// </summary>
    public decimal YoungPersonTaxReliefLimit { get; set; } = TaxRates.YoungPersonTaxReliefLimitDefaultValue;
    
    /// <summary>
    /// Data i czas utworzenia rekordu w systemie.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    public User User { get; set; } = null!;
}