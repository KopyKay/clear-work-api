namespace ClearWork.Domain.Enums;

/// <summary>
/// Środek transportu użyty podczas delegacji.
/// </summary>
public enum TransportType
{
    /// <summary>
    /// Prywatny samochód
    /// </summary>
    PrivateCar,
    
    /// <summary>
    /// Służbowy samochód
    /// </summary>
    CompanyCar,
    
    /// <summary>
    /// Transport publiczny
    /// </summary>
    PublicTransport,
    
    /// <summary>
    /// Samolot
    /// </summary>
    Airplane,
    
    /// <summary>
    /// Motocykl
    /// </summary>
    Motorcycle,
    
    /// <summary>
    /// Motorower
    /// </summary>
    Moped,
    
    Other
}