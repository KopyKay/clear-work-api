using System.Text.Json.Serialization;
using ClearWork.Application.BusinessTrips.Dtos;

namespace ClearWork.Application.TimeEntries.Dtos;

public class WorkEntryDto : TimeEntryDto
{
    [JsonPropertyOrder(3)]
    public int? BusinessTripId { get; set; }
    
    [JsonPropertyOrder(4)]
    public DailyBusinessTripDetailDto? DailyBusinessTripDetail { get; set; }
}