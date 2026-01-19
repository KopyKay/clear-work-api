namespace ClearWork.Domain.Enums;

/// <summary>
/// Typ zastosowanych kosztów uzyskania przychodu.
/// </summary>
public enum TaxDeductionType
{
    None,
    
    /// <summary>
    /// Standardowe
    /// </summary>
    Standard,
    
    /// <summary>
    /// Praca twórcza
    /// </summary>
    CreativeWork,
    
    /// <summary>
    /// Procentowe
    /// </summary>
    PercentageBased,
    
    /// <summary>
    /// Koszty rzeczywiste
    /// </summary>
    ActualCosts,
    
    Other
}