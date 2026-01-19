using ClearWork.Domain.Enums;

namespace ClearWork.Domain.Entities;

/// <summary>
/// Reprezentuje podróż delegację krajową lub zagraniczną.
/// </summary>
public class BusinessTrip
{
    public int Id { get; set; }
    
    /// <summary>
    /// Identyfikator miejsca pracy delegującego pracownika.
    /// </summary>
    public required int EmploymentContractId { get; set; }
    
    /// <summary>
    /// Data i godzina rozpoczęcia delegacji.
    /// </summary>
    public required DateTimeOffset StartDateTime { get; set; }
    
    /// <summary>
    /// Data i godzina zakończenia delegacji.
    /// </summary>
    public DateTimeOffset? EndDateTime { get; set; }
    
    /// <summary>
    /// Typ delegacji:
    /// <list type="bullet">
    ///     <item>Delegacja krajowa</item>
    ///     <item>Delegacja zagraniczna</item>
    /// </list>
    /// </summary>
    public required BusinessTripType TripType { get; set; }
    
    /// <summary>
    /// Kod kraju docelowego ISO 3166-1 alfa-3.
    /// </summary>
    public required string DestinationCountry { get; set; }
    
    /// <summary>
    /// Sposób rozliczenia zakwaterowania podczas delegacji:
    /// <list type="bullet">
    ///     <item>Ryczałt za nocleg</item>
    ///     <item>Zwrot faktycznych kosztów</item>
    ///     <item>Nocleg zapewniony przez pracodawcę</item>
    ///     <item>Brak noclegu</item>
    /// </list>
    /// </summary>
    public required AccommodationType AccommodationType { get; set; }
    
    /// <summary>
    /// Środek transportu użyty podczas delegacji:
    /// <list type="bullet">
    ///     <item>Samochód prywatny</item>
    ///     <item>Samochód służbowy</item>
    ///     <item>Transport publiczny</item>
    ///     <item>Samolot</item>
    ///     <item>Motocykl</item>
    ///     <item>Inny środek transportu</item>
    /// </list>
    /// </summary>
    public required TransportType TransportType { get; set; }
    
    /// <summary>
    /// Liczba przejechanych kilometrów pojazdem prywatnym.
    /// </summary>
    public decimal? KilometersDriven { get; set; }
    
    /// <summary>
    /// Pojemność silnika samochodu prywatnego w cm³.
    /// </summary>
    public int? CarEngineCapacity { get; set; }
    
    /// <summary>
    /// Faktyczny koszt transportu.
    /// </summary>
    public decimal? ActualTransportCost { get; set; }
    
    /// <summary>
    /// Określa, czy przysługuje ryczałt na pokrycie kosztów dojazdu środkami
    /// komunikacji miejscowej.
    /// </summary>
    public bool UseLocalTransportAllowance { get; set; } = false;
    
    /// <summary>
    /// Data i czas utworzenia delegacji.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    public EmploymentContract EmploymentContract { get; set; } = null!;
    public ICollection<WorkEntry> WorkEntries { get; set; } = [];
}