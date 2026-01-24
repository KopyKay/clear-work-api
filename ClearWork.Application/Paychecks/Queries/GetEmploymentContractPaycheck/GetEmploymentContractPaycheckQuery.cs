using ClearWork.Application.Paychecks.Dtos;
using MediatR;

namespace ClearWork.Application.Paychecks.Queries.GetEmploymentContractPaycheck;

public record GetEmploymentContractPaycheckQuery(int WorkplaceId, int ContractId, int PaycheckId) : IRequest<PaycheckDto?>;