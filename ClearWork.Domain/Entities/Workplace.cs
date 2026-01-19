using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Domain.Entities;

/// <summary>
/// Reprezentuje miejsce pracy użytkownika.
/// </summary>
public class Workplace
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator użytkownika będącego pracownikiem tego miejsca pracy.
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Nazwa miejsca pracy.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Domyślna stawka godzinowa
    /// </summary>
    public required decimal BaseHourlyRate { get; set; }
    
    /// <summary>
    /// Data i czas dodania miejsca pracy.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Określa, czy miejsce pracy jest aktualne.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Ustawienia PPK (Pracowniczych Planów Kapitałowych) dla tego miejsca pracy.
    /// </summary>
    public PpkSetting? PpkSettings { get; set; }
    
    public User User { get; set; } = null!;
    public ICollection<EmploymentContract> EmploymentContracts { get; set; } = [];
}