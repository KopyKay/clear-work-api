using System.Text.Json.Serialization;

namespace ClearWork.Application.TimeEntries.Dtos;

public class WorkEntryDto : TimeEntryDto
{
    [JsonPropertyOrder(3)]
    public int? BusinessTripId { get; set; }
    
    [JsonPropertyOrder(4)]
    public DailyBusinessTripDetailDto? DailyBusinessTripDetail { get; set; }
}