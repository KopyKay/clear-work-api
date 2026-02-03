using System.Text.Json.Serialization;
using ClearWork.Application.Workplaces.Dtos;
using MediatR;

namespace ClearWork.Application.Workplaces.Commands.UpdateUserWorkplace;

public record UpdateUserWorkplaceCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public string? Name { get; init; }
    public decimal? BaseHourlyRate { get; init; }
    public UpdatePpkSettingDto? PpkSettings { get; init; }
    public bool? IsActive { get; init; }
}