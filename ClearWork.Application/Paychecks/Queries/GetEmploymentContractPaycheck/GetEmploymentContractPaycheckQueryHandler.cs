using AutoMapper;
using ClearWork.Application.Paychecks.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
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
        
        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId)
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");
        
        _ = await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");

        var employmentContractPaycheck =
            await paycheckRepository.GetEmploymentContractPaycheckAsync(request.ContractId, request.PaycheckId)
            ?? throw new NotFoundException(nameof(Paycheck),
                $"{request.PaycheckId.ToString()} for this employment contract");

        var employmentContractPaycheckDto = mapper.Map<PaycheckDto>(employmentContractPaycheck);

        return employmentContractPaycheckDto;
    }
}