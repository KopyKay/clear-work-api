using AutoMapper;
using ClearWork.Application.EmploymentContracts.Dtos;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.EmploymentContracts.Queries.GetWorkplaceEmploymentContracts;

public class GetWorkplaceEmploymentContractsQueryHandler 
(
    ILogger<GetWorkplaceEmploymentContractsQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetWorkplaceEmploymentContractsQuery, IEnumerable<EmploymentContractDto>>
{
    public async Task<IEnumerable<EmploymentContractDto>> Handle(GetWorkplaceEmploymentContractsQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting employment contracts from workplace with id [{WorkplaceId}]", request.WorkplaceId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        var workplaceEmploymentContracts =
            await employmentContractRepository.GetWorkplaceEmploymentContractsAsync(request.WorkplaceId);

        var workplaceEmploymentContractsDtos = mapper.Map<IEnumerable<EmploymentContractDto>>(workplaceEmploymentContracts);
        
        return workplaceEmploymentContractsDtos;
    }
}