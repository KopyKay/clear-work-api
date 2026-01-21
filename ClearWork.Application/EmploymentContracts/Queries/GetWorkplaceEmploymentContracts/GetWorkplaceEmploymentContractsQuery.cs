using ClearWork.Application.EmploymentContracts.Dtos;
using MediatR;

namespace ClearWork.Application.EmploymentContracts.Queries.GetWorkplaceEmploymentContracts;

public record GetWorkplaceEmploymentContractsQuery(int WorkplaceId) : IRequest<IEnumerable<EmploymentContractDto>>;