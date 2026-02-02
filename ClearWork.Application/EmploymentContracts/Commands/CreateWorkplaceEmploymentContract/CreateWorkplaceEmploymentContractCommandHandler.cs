using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.EmploymentContracts.Commands.CreateWorkplaceEmploymentContract;

public class CreateWorkplaceEmploymentContractCommandHandler 
(
    ILogger<CreateWorkplaceEmploymentContractCommandHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<CreateWorkplaceEmploymentContractCommand, int>
{
    public async Task<int> Handle(CreateWorkplaceEmploymentContractCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Creating employment contract in workplace with id [{WorkplaceId}]", request.WorkplaceId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        var workplaceEmploymentContract = mapper.Map<EmploymentContract>(request);
        workplaceEmploymentContract.WorkplaceId = request.WorkplaceId;

        var employmentContractId =
            await employmentContractRepository.CreateWorkplaceEmploymentContractAsync(workplaceEmploymentContract);
        return employmentContractId;
    }
}