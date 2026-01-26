using AutoMapper;
using ClearWork.Application.BusinessTrips.Dtos;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.BusinessTrips.Queries.GetEmploymentContractBusinessTrips;

public class GetEmploymentContractBusinessTripsQueryHandler 
(
    ILogger<GetEmploymentContractBusinessTripsQueryHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IBusinessTripRepository businessTripRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetEmploymentContractBusinessTripsQuery, IEnumerable<BusinessTripDto>>
{
    public async Task<IEnumerable<BusinessTripDto>> Handle(GetEmploymentContractBusinessTripsQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Getting business trips from employment contract with id [{ContractId}]", request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var employmentContractBusinessTrips =
            await businessTripRepository.GetEmploymentContractBusinessTripsAsync(request.ContractId);

        var employmentContractBusinessTripsDtos = mapper.Map<IEnumerable<BusinessTripDto>>(employmentContractBusinessTrips);
        
        return employmentContractBusinessTripsDtos;
    }
}