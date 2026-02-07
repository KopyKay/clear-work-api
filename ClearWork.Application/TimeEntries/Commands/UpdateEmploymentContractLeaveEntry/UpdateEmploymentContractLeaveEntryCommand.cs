using System.Text.Json.Serialization;
using ClearWork.Domain.Enums;
using MediatR;

namespace ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractLeaveEntry;

public record UpdateEmploymentContractLeaveEntryCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [JsonIgnore]
    public int WorkplaceId { get; set; }
    
    [JsonIgnore]
    public int ContractId { get; set; }
    
    public DateTimeOffset? StartDateTime { get; init; }
    public DateTimeOffset? EndDateTime { get; init; }
    public string? TextNote { get; init; }
    public LeaveType? LeaveType { get; init; }
    public int? WorkingDays { get; init; }
}