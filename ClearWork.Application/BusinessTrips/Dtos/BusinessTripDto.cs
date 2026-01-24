using ClearWork.Domain.Enums;

namespace ClearWork.Application.BusinessTrips.Dtos;

public class BusinessTripDto
{
    public int Id { get; set; }
    public int EmploymentContractId { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset? EndDateTime { get; set; }
    public BusinessTripType TripType { get; set; }
    public string DestinationCountry { get; set; } = null!;
    public AccommodationType AccommodationType { get; set; }
    public TransportType TransportType { get; set; }
    public decimal? KilometersDriven { get; set; }
    public int? CarEngineCapacity { get; set; }
    public decimal? ActualTransportCost { get; set; }
    public bool UseLocalTransportAllowance { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}