using ClearWork.Application.Workplaces.Dtos;
using MediatR;

namespace ClearWork.Application.Workplaces.Queries.GetUserWorkplaces;

public record GetUserWorkplacesQuery : IRequest<IEnumerable<WorkplaceDto>>;