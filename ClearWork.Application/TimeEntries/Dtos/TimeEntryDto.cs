using System.Text.Json.Serialization;

namespace ClearWork.Application.TimeEntries.Dtos;

[JsonDerivedType(typeof(WorkEntryDto))]
[JsonDerivedType(typeof(LeaveEntryDto))]
public class TimeEntryDto
{
    public int Id { get; set; }
    public int EmploymentContractId { get; set; }
    public string EntryType { get; set; } = null!;
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }
    public string? TextNote { get; set; }
    public bool HasAudioNote { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
}