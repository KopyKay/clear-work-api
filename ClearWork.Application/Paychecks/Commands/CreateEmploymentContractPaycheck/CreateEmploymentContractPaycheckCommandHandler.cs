using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Paychecks.Commands.CreateEmploymentContractPaycheck;

public class CreateEmploymentContractPaycheckCommandHandler 
(
    ILogger<CreateEmploymentContractPaycheckCommandHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IPaycheckRepository paycheckRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<CreateEmploymentContractPaycheckCommand, int>
{
    public async Task<int> Handle(CreateEmploymentContractPaycheckCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Creating payment in employment contract with id [{ContractId}]", request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        var employmentContractPaycheck = mapper.Map<Paycheck>(request);
        employmentContractPaycheck.EmploymentContractId = request.ContractId;
        
        var paycheckId = await paycheckRepository.CreateEmploymentContractPaycheckAsync(employmentContractPaycheck);
        return paycheckId;
    }
}