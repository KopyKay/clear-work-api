using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.TimeEntries.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.TimeEntries.Queries.GetEmploymentContractTimeEntries;

public class GetEmploymentContractTimeEntriesQueryHandler
(
    ILogger<GetEmploymentContractTimeEntriesQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    ITimeEntryRepository timeEntryRepository,
    IUserContext userContext,
    IMapper mapper
)
: IRequestHandler<GetEmploymentContractTimeEntriesQuery, IEnumerable<TimeEntryDto>>
{
    public async Task<IEnumerable<TimeEntryDto>> Handle(GetEmploymentContractTimeEntriesQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting time entries from employment contract with id [{ContractId}]", request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        var employmentContractTimeEntries = 
            await timeEntryRepository.GetEmploymentContractTimeEntriesAsync(request.ContractId);

        var employmentContractTimeEntriesDtos = 
            mapper.Map<IEnumerable<TimeEntryDto>>(employmentContractTimeEntries);
        
        return employmentContractTimeEntriesDtos;
    }
}