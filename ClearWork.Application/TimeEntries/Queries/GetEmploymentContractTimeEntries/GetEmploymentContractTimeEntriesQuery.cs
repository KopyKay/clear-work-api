using ClearWork.Application.TimeEntries.Dtos;
using MediatR;

namespace ClearWork.Application.TimeEntries.Queries.GetEmploymentContractTimeEntries;

public record GetEmploymentContractTimeEntriesQuery(int WorkplaceId, int ContractId) : IRequest<IEnumerable<TimeEntryDto>>;