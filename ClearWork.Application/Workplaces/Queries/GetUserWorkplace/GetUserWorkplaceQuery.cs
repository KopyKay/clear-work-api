using ClearWork.Application.Workplaces.Dtos;
using MediatR;

namespace ClearWork.Application.Workplaces.Queries.GetUserWorkplace;

public record GetUserWorkplaceQuery(int WorkplaceId) : IRequest<WorkplaceDto?>;