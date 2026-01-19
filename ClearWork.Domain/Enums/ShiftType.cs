namespace ClearWork.Domain.Enums;

/// <summary>
/// Typ pracy określający mnożnik wynagrodzenia.
/// </summary>
public enum ShiftType
{
    /// <summary>
    /// Standardowe godziny pracy
    /// </summary>
    Normal,
        
    /// <summary>
    /// Praca w godzinach nadliczbowych
    /// </summary>    
    Overtime,
    
    /// <summary>
    /// Praca w porze nocnej
    /// </summary>
    Night,
    
    /// <summary>
    /// Praca w niedzielę lub święto
    /// </summary>
    SundayOrHoliday,
    
    /// <summary>
    /// Praca w sobotę
    /// </summary>
    Saturday
}