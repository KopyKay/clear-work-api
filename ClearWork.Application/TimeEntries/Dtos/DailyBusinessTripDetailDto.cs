namespace ClearWork.Application.TimeEntries.Dtos;

public class DailyBusinessTripDetailDto
{
    public bool ProvidedBreakfast { get; set; }
    public bool ProvidedLunch { get; set; }
    public bool ProvidedDinner { get; set; }
    public bool HasOvernightStay { get; set; }
    public decimal? ActualAccommodationCostThisDay { get; set; }
}