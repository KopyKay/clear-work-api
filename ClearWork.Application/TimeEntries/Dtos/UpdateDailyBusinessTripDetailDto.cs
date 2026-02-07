namespace ClearWork.Application.TimeEntries.Dtos;

public record UpdateDailyBusinessTripDetailDto
{
    public bool? ProvidedBreakfast { get; init; }
    public bool? ProvidedLunch { get; init; }
    public bool? ProvidedDinner { get; init; }
    public bool? HasOvernightStay { get; init; }
    public decimal? ActualAccommodationCostThisDay { get; init; }
}