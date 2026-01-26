namespace ClearWork.Application.Workplaces.Dtos;

public class WorkplaceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal BaseHourlyRate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public PpkSettingDto? PpkSettings { get; set; }
}