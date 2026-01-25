using AutoMapper;
using ClearWork.Application.TimeEntries.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
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
        
        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId)
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");
        
        _ = await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");
        
        var employmentContractTimeEntries = 
            await timeEntryRepository.GetEmploymentContractTimeEntriesAsync(request.ContractId);

        var employmentContractTimeEntriesDtos = 
            mapper.Map<IEnumerable<TimeEntryDto>>(employmentContractTimeEntries);
        
        return employmentContractTimeEntriesDtos;
    }
}