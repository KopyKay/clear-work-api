namespace ClearWork.Domain.Enums;

/// <summary>
/// Typ umowy określający zasady rozliczeń składek ZUS i podatku.
/// </summary>
public enum ContractType
{
    /// <summary>
    /// Umowa o pracę
    /// </summary>
    UoP,
        
    /// <summary>
    /// Umowa zlecenie
    /// </summary>    
    Uz,
    
    /// <summary>
    /// Umowa o dzieło
    /// </summary>
    UoD,
    
    /// <summary>
    /// Współpraca B2B - własna działalność gospodarcza
    /// </summary>
    B2B
}