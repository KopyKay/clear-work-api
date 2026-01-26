using System.Text.Json.Serialization;
using ClearWork.Domain.Enums;

namespace ClearWork.Application.TimeEntries.Dtos;

public class LeaveEntryDto : TimeEntryDto
{
    [JsonPropertyOrder(3)]
    public LeaveType LeaveType { get; set; }
    
    [JsonPropertyOrder(4)]
    public int WorkingDays { get; set; }
}