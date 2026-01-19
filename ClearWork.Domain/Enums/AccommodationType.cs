namespace ClearWork.Domain.Enums;

/// <summary>
/// Sposób rozliczenia zakwaterowania podczas delegacji.
/// </summary>
public enum AccommodationType
{
    /// <summary>
    /// Brak noclegu
    /// </summary>
    None,
    
    /// <summary>
    /// Ryczałt za nocleg
    /// </summary>
    FlatRate,
    
    /// <summary>
    /// Zwrot faktycznych kosztów
    /// </summary>
    Reimbursement,
    
    /// <summary>
    /// Nocleg zapewniony przez pracodawcę
    /// </summary>
    ProvidedByEmployer
}