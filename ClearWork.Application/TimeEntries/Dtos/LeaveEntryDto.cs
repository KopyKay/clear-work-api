using ClearWork.Domain.Enums;

namespace ClearWork.Application.TimeEntries.Dtos;

public class LeaveEntryDto : TimeEntryDto
{
    public LeaveType LeaveType { get; set; }
    public int WorkingDays { get; set; }
}