using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.EmploymentContracts.Commands.UpdateWorkplaceEmploymentContract;

public class UpdateWorkplaceEmploymentContractCommandHandler 
(
    ILogger<UpdateWorkplaceEmploymentContractCommandHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<UpdateWorkplaceEmploymentContractCommand>
{
    public async Task Handle(UpdateWorkplaceEmploymentContractCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating employment contract [{ContractId}] in workplace [{WorkplaceId}] with {@UpdatedContract}",
            request.Id, request.WorkplaceId, request);
        
        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        var workplaceEmploymentContract =
            await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.Id, true)
            ?? throw NotFoundException.ForChildResource<EmploymentContract, Workplace>(request.Id, request.WorkplaceId);

        ValidateDateChanges(request, workplaceEmploymentContract);
        
        mapper.Map(request, workplaceEmploymentContract);
        
        await employmentContractRepository.SaveChangesAsync();
    }
    
    private static void ValidateDateChanges(UpdateWorkplaceEmploymentContractCommand request, EmploymentContract existing)
    {
        var newStartDate = request.StartDateTime?.ToUniversalTime() ?? existing.StartDateTime;
        var newEndDate = request.EndDateTime?.ToUniversalTime() ?? existing.EndDateTime;

        if (newEndDate.HasValue && newEndDate <= newStartDate)
            throw new InvalidDateRangeException();
    }
}