using AutoMapper;
using ClearWork.Application.BusinessTrips.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
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
        
        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId)
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");
        
        _ = await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");

        var employmentContractBusinessTrips =
            await businessTripRepository.GetEmploymentContractBusinessTripsAsync(request.ContractId);

        var employmentContractBusinessTripsDtos = mapper.Map<IEnumerable<BusinessTripDto>>(employmentContractBusinessTrips);
        
        return employmentContractBusinessTripsDtos;
    }
}