using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.BusinessTrips.Commands.CreateEmploymentContractBusinessTrip;

public class CreateEmploymentContractBusinessTripCommandHandler 
(
    ILogger<CreateEmploymentContractBusinessTripCommandHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IBusinessTripRepository businessTripRepository,
    IUserContext userContext,
    IMapper mapper   
)    
: IRequestHandler<CreateEmploymentContractBusinessTripCommand, int>
{
    public async Task<int> Handle(CreateEmploymentContractBusinessTripCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Creating business trip in employment contract with id [{ContractId}]", request.ContractId);
        
        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        var businessTrip = mapper.Map<BusinessTrip>(request);
        businessTrip.EmploymentContractId = request.ContractId;

        var businessTripId = await businessTripRepository.CreateEmploymentContractBusinessTripAsync(businessTrip);
        return businessTripId;
    }
}