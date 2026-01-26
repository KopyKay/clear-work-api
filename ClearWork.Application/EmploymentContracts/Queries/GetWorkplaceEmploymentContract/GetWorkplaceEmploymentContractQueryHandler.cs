using AutoMapper;
using ClearWork.Application.EmploymentContracts.Dtos;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.EmploymentContracts.Queries.GetWorkplaceEmploymentContract;

public class GetWorkplaceEmploymentContractQueryHandler 
(
    ILogger<GetWorkplaceEmploymentContractQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetWorkplaceEmploymentContractQuery, EmploymentContractDto?>
{
    public async Task<EmploymentContractDto?> Handle(GetWorkplaceEmploymentContractQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting employment contract with id [{ContractId}] from workplace with id [{WorkplaceId}]",
            request.ContractId, request.WorkplaceId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        var workplaceEmploymentContract =
            await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        var workplaceEmploymentContractDto = mapper.Map<EmploymentContractDto>(workplaceEmploymentContract);
        
        return workplaceEmploymentContractDto;
    }
}