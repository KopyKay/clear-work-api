using ClearWork.Application.EmploymentContracts.Dtos;
using MediatR;

namespace ClearWork.Application.EmploymentContracts.Queries.GetWorkplaceEmploymentContract;

public record GetWorkplaceEmploymentContractQuery(int WorkplaceId, int ContractId) : IRequest<EmploymentContractDto?>;