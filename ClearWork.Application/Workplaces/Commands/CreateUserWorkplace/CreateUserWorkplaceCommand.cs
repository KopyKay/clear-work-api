using ClearWork.Application.Workplaces.Dtos;
using MediatR;

namespace ClearWork.Application.Workplaces.Commands.CreateUserWorkplace;

public record CreateUserWorkplaceCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
    public decimal BaseHourlyRate { get; init; }
    public PpkSettingDto? PpkSettings { get; init; }
}