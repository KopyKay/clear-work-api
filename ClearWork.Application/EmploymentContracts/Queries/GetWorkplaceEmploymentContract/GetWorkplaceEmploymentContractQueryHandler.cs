using AutoMapper;
using ClearWork.Application.EmploymentContracts.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
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
        
        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId) 
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");

        var workplaceEmploymentContract =
            await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");
        
        var workplaceEmploymentContractDto = mapper.Map<EmploymentContractDto>(workplaceEmploymentContract);
        
        return workplaceEmploymentContractDto;
    }
}