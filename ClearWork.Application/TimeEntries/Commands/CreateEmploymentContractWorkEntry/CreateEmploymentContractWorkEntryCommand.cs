using System.Text.Json.Serialization;
using ClearWork.Application.TimeEntries.Dtos;
using ClearWork.Domain.Enums;
using MediatR;

namespace ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractWorkEntry;

public record CreateEmploymentContractWorkEntryCommand : IRequest<int>
{
    [JsonIgnore]
    public int WorkplaceId { get; set; }
    
    [JsonIgnore]
    public int ContractId { get; set; }
    
    public DateTimeOffset StartDateTime { get; init; }
    public DateTimeOffset EndDateTime { get; init; }
    public string? TextNote { get; init; }
    public int? BusinessTripId { get; init; }
    public CreateDailyBusinessTripDetailDto? DailyBusinessTripDetail { get; init; }
}