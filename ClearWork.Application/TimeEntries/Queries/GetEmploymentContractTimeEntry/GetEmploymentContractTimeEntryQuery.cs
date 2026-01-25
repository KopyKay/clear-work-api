using ClearWork.Application.TimeEntries.Dtos;
using MediatR;

namespace ClearWork.Application.TimeEntries.Queries.GetEmploymentContractTimeEntry;

public record GetEmploymentContractTimeEntryQuery(int WorkplaceId, int ContractId, int TimeEntryId) 
    : IRequest<TimeEntryDto?>;