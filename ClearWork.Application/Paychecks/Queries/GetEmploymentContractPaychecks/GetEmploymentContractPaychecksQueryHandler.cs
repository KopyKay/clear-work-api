using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Paychecks.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Paychecks.Queries.GetEmploymentContractPaychecks;

public class GetEmploymentContractPaychecksQueryHandler 
(
    ILogger<GetEmploymentContractPaychecksQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IPaycheckRepository paycheckRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetEmploymentContractPaychecksQuery, IEnumerable<PaycheckDto>>
{
    public async Task<IEnumerable<PaycheckDto>> Handle(GetEmploymentContractPaychecksQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting paychecks from employment contract with id [{ContractId}]", request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var employmentContractPaychecks = 
            await paycheckRepository.GetEmploymentContractPaychecksAsync(request.ContractId);
        
        var employmentContractPaychecksDtos = mapper.Map<IEnumerable<PaycheckDto>>(employmentContractPaychecks);

        return employmentContractPaychecksDtos;
    }
}