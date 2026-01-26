using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Paychecks.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Paychecks.Queries.GetEmploymentContractPaycheck;

public class GetEmploymentContractPaycheckQueryHandler 
(
    ILogger<GetEmploymentContractPaycheckQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IPaycheckRepository paycheckRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetEmploymentContractPaycheckQuery, PaycheckDto?>
{
    public async Task<PaycheckDto?> Handle(GetEmploymentContractPaycheckQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting paycheck with id [{PaycheckId}] from employment contract with id [{ContractId}]",
            request.PaycheckId, request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var employmentContractPaycheck =
            await paycheckRepository.GetEmploymentContractPaycheckOrThrowAsync(request.ContractId, request.PaycheckId);

        var employmentContractPaycheckDto = mapper.Map<PaycheckDto>(employmentContractPaycheck);

        return employmentContractPaycheckDto;
    }
}