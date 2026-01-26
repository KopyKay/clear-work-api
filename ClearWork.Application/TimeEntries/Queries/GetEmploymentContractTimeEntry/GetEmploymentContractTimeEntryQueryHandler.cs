using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.TimeEntries.Dtos;
using ClearWork.Application.Users;
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

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var employmentContractTimeEntry =
            await timeEntryRepository.GetEmploymentContractTimeEntryOrThrowAsync(request.ContractId, request.TimeEntryId);
        
        var employmentContractTimeEntryDto = mapper.Map<TimeEntryDto>(employmentContractTimeEntry);
        
        return employmentContractTimeEntryDto;
    }
}