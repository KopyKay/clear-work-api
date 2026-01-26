using AutoMapper;
using ClearWork.Application.BusinessTrips.Dtos;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.BusinessTrips.Queries.GetEmploymentContractBusinessTrip;

public class GetEmploymentContractBusinessTripQueryHandler 
(
    ILogger<GetEmploymentContractBusinessTripQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IBusinessTripRepository businessTripRepository,
    IUserContext userContext,
    IMapper mapper    
)    
: IRequestHandler<GetEmploymentContractBusinessTripQuery, BusinessTripDto?>
{
    public async Task<BusinessTripDto?> Handle(GetEmploymentContractBusinessTripQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting business trip with id [{BusinessTripId}] from employment contract with id [{ContractId}]",
            request.BusinessTripId, request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var employmentContractBusinessTrip =
            await businessTripRepository.GetEmploymentContractBusinessTripOrThrowAsync(request.ContractId, request.BusinessTripId);

        var employmentContractBusinessTripDto = mapper.Map<BusinessTripDto>(employmentContractBusinessTrip);

        return employmentContractBusinessTripDto;
    }
}