using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Paychecks.Commands.UpdateEmploymentContractPaycheck;

public class UpdateEmploymentContractPaycheckCommandHandler 
(
    ILogger<UpdateEmploymentContractPaycheckCommandHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IPaycheckRepository paycheckRepository,
    IUserContext userContext,
    IMapper mapper
)
: IRequestHandler<UpdateEmploymentContractPaycheckCommand>
{
    public async Task Handle(UpdateEmploymentContractPaycheckCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating paycheck [{PaycheckId}] in employment contract [{ContractId}] with {@UpdatedPayment}",
            request.Id, request.ContractId, request);
        
        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        var employmentContractPaycheck =
            await paycheckRepository.GetEmploymentContractPaycheckAsync(request.ContractId, request.Id, true)
            ?? throw NotFoundException.ForChildResource<Paycheck, EmploymentContract>(request.Id, request.ContractId);
        
        mapper.Map(request, employmentContractPaycheck);
        
        await paycheckRepository.SaveChangesAsync();
    }
}