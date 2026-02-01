namespace ClearWork.Application.Workplaces.Dtos;

public record UpdatePpkSettingDto
{
    public bool? IsActive { get; init; }
    public decimal? EmployeeRate { get; init; }
    public decimal? EmployerRate { get; init; }
}