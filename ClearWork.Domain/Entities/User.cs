using ClearWork.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ClearWork.Domain.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateOnly DateOfBirth { get; set; }
    
    /// <summary>
    /// Określa, czy użytkownik posiada status studenta.
    /// </summary>
    public bool IsStudent { get; set; } = false;
    
    /// <summary>
    /// Stopień niepełnosprawności użytkownika zgodnie z ustawą o rehabilitacji zawodowej.
    /// </summary>
    public DisabilityLevel? DisabilityLevel { get; set; }
    
    /// <summary>
    /// Data i czas utworzenia konta użytkownika.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Określa, czy konto użytkownika jest aktywne.
    /// Nieaktywne konta zachowują dane historyczne, ale nie pozwalają na nowe operacje.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    public AppSetting AppSetting { get; set; } = null!;
    public ICollection<AnnualTaxRate> AnnualTaxRate { get; set; } = [];
    public ICollection<Workplace> Workplaces { get; set; } = [];
}