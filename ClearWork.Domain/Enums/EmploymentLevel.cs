namespace ClearWork.Domain.Enums;

/// <summary>
/// Wymiar etatu określający proporcję czasu pracy w stosunku do pełnego etatu.
/// </summary>
public enum EmploymentLevel
{
    /// <summary>
    /// Pełny etat
    /// </summary>
    Full,
        
    /// <summary>
    /// 3/4 etatu
    /// </summary>
    ThreeQuarter,
    
    /// <summary>
    /// 1/2 etatu
    /// </summary>
    Half,
    
    /// <summary>
    /// 1/4 etatu
    /// </summary>
    Quarter
}