using ClearWork.Application.BusinessTrips.Dtos;
using MediatR;

namespace ClearWork.Application.BusinessTrips.Queries.GetEmploymentContractBusinessTrips;

public record GetEmploymentContractBusinessTripsQuery(int WorkplaceId, int ContractId) : IRequest<IEnumerable<BusinessTripDto>>;