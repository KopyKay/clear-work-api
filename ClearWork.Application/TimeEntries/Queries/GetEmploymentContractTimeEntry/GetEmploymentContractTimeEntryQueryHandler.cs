using AutoMapper;
using ClearWork.Application.TimeEntries.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.TimeEntries.Queries.GetEmploymentContractTimeEntry;

public class GetEmploymentContractTimeEntryQueryHandler 
(
    ILogger<GetEmploymentContractTimeEntryQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    ITimeEntryRepository timeEntryRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetEmploymentContractTimeEntryQuery, TimeEntryDto?>
{
    public async Task<TimeEntryDto?> Handle(GetEmploymentContractTimeEntryQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting time entry with id [{TimeEntryId}] from employment contract with id [{ContractId}]",
            request.TimeEntryId, request.ContractId);
        
        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId)
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");
        
        _ = await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");
        
        var employmentContractTimeEntry =
            await timeEntryRepository.GetEmploymentContractTimeEntryAsync(request.ContractId, request.TimeEntryId)
            ?? throw new NotFoundException(nameof(TimeEntry),
                $"{request.TimeEntryId.ToString()} for this employment contract");
        
        var employmentContractTimeEntryDto = mapper.Map<TimeEntryDto>(employmentContractTimeEntry);
        
        return employmentContractTimeEntryDto;
    }
}