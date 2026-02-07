using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.Users;
using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Enums;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.BusinessTrips.Commands.UpdateEmploymentContractBusinessTrip;

public class UpdateEmploymentContractBusinessTripCommandHandler 
(
    ILogger<UpdateEmploymentContractBusinessTripCommandHandler> logger,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IBusinessTripRepository businessTripRepository,
    IUserContext userContext,
    IMapper mapper  
)    
: IRequestHandler<UpdateEmploymentContractBusinessTripCommand>
{
    public async Task Handle(UpdateEmploymentContractBusinessTripCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating business trip [{BusinessTripId}] in employment contract [{ContractId}] with {@UpdatedBusinessTrip}",
            request.Id, request.ContractId, request);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var businessTrip = await businessTripRepository.GetEmploymentContractBusinessTripAsync(request.ContractId, request.Id, true) 
                           ?? throw NotFoundException.ForChildResource<BusinessTrip, EmploymentContract>(request.Id, request.ContractId);

        ValidateDateChanges(request, businessTrip);
        ValidateTripTypeConsistency(request, businessTrip);
        ValidateTransportTypeConsistency(request, businessTrip);
        
        mapper.Map(request, businessTrip);

        await businessTripRepository.SaveChangesAsync();
    }
    
    private static void ValidateDateChanges(UpdateEmploymentContractBusinessTripCommand request, BusinessTrip existing)
    {
        var newStartDate = request.StartDateTime?.ToUniversalTime() ?? existing.StartDateTime;
        var newEndDate = request.EndDateTime?.ToUniversalTime() ?? existing.EndDateTime;

        if (newEndDate.HasValue && newEndDate <= newStartDate)
            throw new InvalidDateRangeException();
    }
    
    private static void ValidateTripTypeConsistency(UpdateEmploymentContractBusinessTripCommand request, BusinessTrip existing)
    {
        var newTripType = request.TripType ?? existing.TripType;
        var newCountry = request.DestinationCountry ?? existing.DestinationCountry;

        var isPoland = newCountry.Equals(CountryCodes.Poland, StringComparison.OrdinalIgnoreCase);
        var isConsistent = newTripType switch
        {
            BusinessTripType.Domestic => isPoland,
            BusinessTripType.International => !isPoland,
            _ => false
        };

        if (!isConsistent)
            throw new BusinessRuleException(
                "Trip type must be consistent with destination country. " +
                "Use 'Domestic' for Poland (POL) and 'International' for other countries.");
    }

    private static void ValidateTransportTypeConsistency(UpdateEmploymentContractBusinessTripCommand request, BusinessTrip existing)
    {
        var newTransportType = request.TransportType ?? existing.TransportType;
        
        if (request.KilometersDriven.HasValue)
        {
            var validTransportTypes = new[] { TransportType.PrivateCar, TransportType.Motorcycle, TransportType.Moped };
            if (!validTransportTypes.Contains(newTransportType))
                throw new BusinessRuleException("Kilometers driven can only be specified for private car, motorcycle, or moped.");
        }
        
        if (request.CarEngineCapacity.HasValue && newTransportType != TransportType.PrivateCar)
            throw new BusinessRuleException("Car engine capacity can only be specified for private car.");
    }
}