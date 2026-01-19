namespace ClearWork.Domain.Entities;

/// <summary>
/// Reprezentuje rekord wypłaty wynagrodzenia otrzymanego przez użytkownika.
/// Służy do śledzenia i weryfikacji faktycznie otrzymanych kwot
/// w porównaniu z obliczeniami systemowymi.
/// </summary>
public class Paycheck
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator umowy, w ramach której dokonano wypłaty.
    /// </summary>
    public required int EmploymentContractId { get; set; }
    
    /// <summary>
    /// Data otrzymania wynagrodzenia.
    /// </summary>
    public required DateOnly PaymentDate { get; set; }
    
    /// <summary>
    /// Kwota brutto wypłaty.
    /// </summary>
    public required decimal GrossAmount { get; set; }
    
    /// <summary>
    /// Kwota netto wypłaty.
    /// </summary>
    public required decimal NetAmount { get; set; }
    
    /// <summary>
    /// Data i czas dodania wypłaty.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Data i czas ostatniej modyfikacji wypłaty.
    /// </summary>
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public EmploymentContract EmploymentContract { get; set; } = null!;
}