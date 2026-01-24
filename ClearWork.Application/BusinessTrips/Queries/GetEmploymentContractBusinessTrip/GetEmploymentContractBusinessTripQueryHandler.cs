using AutoMapper;
using ClearWork.Application.BusinessTrips.Dtos;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
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
        
        _ = await workplaceRepository.GetUserWorkplaceAsync(userId, request.WorkplaceId)
            ?? throw new NotFoundException(nameof(Workplace),
                $"{request.WorkplaceId.ToString()} for this user");
        
        _ = await employmentContractRepository.GetWorkplaceEmploymentContractAsync(request.WorkplaceId, request.ContractId)
            ?? throw new NotFoundException(nameof(EmploymentContract),
                $"{request.ContractId.ToString()} for this workplace");

        var employmentContractBusinessTrip = 
            await businessTripRepository.GetEmploymentContractBusinessTripAsync(request.ContractId, request.BusinessTripId)
            ?? throw new NotFoundException(nameof(BusinessTrip),
                $"{request.BusinessTripId.ToString()} for this employment contract");

        var employmentContractBusinessTripDto = mapper.Map<BusinessTripDto>(employmentContractBusinessTrip);

        return employmentContractBusinessTripDto;
    }
}