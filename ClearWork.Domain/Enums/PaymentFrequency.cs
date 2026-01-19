namespace ClearWork.Domain.Enums;

/// <summary>
/// Częstotliwość wypłaty wynagrodzenia.
/// </summary>
public enum PaymentFrequency
{
    /// <summary>
    /// Miesięczna
    /// </summary>
    Monthly,
    
    /// <summary>
    /// Co dwa tygodnie
    /// </summary>
    BiWeekly,
    
    /// <summary>
    /// Tygodniowo
    /// </summary>
    Weekly,
    
    /// <summary>
    /// Dzienna
    /// </summary>
    Daily
}