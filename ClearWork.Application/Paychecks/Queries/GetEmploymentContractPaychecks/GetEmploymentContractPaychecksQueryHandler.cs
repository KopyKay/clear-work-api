using AutoMapper;
using ClearWork.Application.Paychecks.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
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

        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId)
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");
        
        _ = await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");

        var employmentContractPaychecks = 
            await paycheckRepository.GetEmploymentContractPaychecksAsync(request.ContractId);
        
        var employmentContractPaychecksDtos = mapper.Map<IEnumerable<PaycheckDto>>(employmentContractPaychecks);

        return employmentContractPaychecksDtos;
    }
}