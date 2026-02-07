using System.Text.Json.Serialization;
using ClearWork.Domain.Enums;
using MediatR;

namespace ClearWork.Application.BusinessTrips.Commands.CreateEmploymentContractBusinessTrip;

public record CreateEmploymentContractBusinessTripCommand : IRequest<int>
{
    [JsonIgnore]
    public int WorkplaceId { get; set; }
    
    [JsonIgnore]
    public int ContractId { get; set; }
    
    public DateTimeOffset StartDateTime { get; init; }
    public DateTimeOffset? EndDateTime { get; init; }
    public BusinessTripType TripType { get; init; }
    public string DestinationCountry { get; init; } = null!;
    public AccommodationType AccommodationType { get; init; }
    public TransportType TransportType { get; init; }
    public decimal? KilometersDriven { get; init; }
    public int? CarEngineCapacity { get; init; }
    public decimal? ActualTransportCost { get; init; }
    public bool UseLocalTransportAllowance { get; init; }
}