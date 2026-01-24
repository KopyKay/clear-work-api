using ClearWork.Application.Paychecks.Dtos;
using MediatR;

namespace ClearWork.Application.Paychecks.Queries.GetEmploymentContractPaychecks;

public record GetEmploymentContractPaychecksQuery(int WorkplaceId, int ContractId) : IRequest<IEnumerable<PaycheckDto>>;