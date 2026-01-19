namespace ClearWork.Domain.OwnedTypes;

/// <summary>
/// Szczegóły pojedynczego dnia delegacji służbowej.
/// </summary>
public class DailyBusinessTripDetail
{
    /// <summary>
    /// Określa, czy pracodawca zapewnił śniadanie tego dnia.
    /// </summary>
    public bool ProvidedBreakfast { get; set; } = false;
    
    /// <summary>
    /// Określa, czy pracodawca zapewnił obiad tego dnia.
    /// </summary>
    public bool ProvidedLunch { get; set; } = false;
    
    /// <summary>
    /// Określa, czy pracodawca zapewnił kolację tego dnia.
    /// </summary>
    public bool ProvidedDinner { get; set; } = false;
    
    /// <summary>
    /// Określa, czy pracownik nocował podczas delegacji tego dnia.
    /// </summary>
    public bool HasOvernightStay { get; set; } = false;
    
    /// <summary>
    /// Faktyczny koszt noclegu tego dnia.
    /// </summary>
    public decimal? ActualAccommodationCostThisDay { get; set; }
}