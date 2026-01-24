using ClearWork.Application.BusinessTrips.Dtos;
using MediatR;

namespace ClearWork.Application.BusinessTrips.Queries.GetEmploymentContractBusinessTrip;

public record GetEmploymentContractBusinessTripQuery(int WorkplaceId, int ContractId, int BusinessTripId) : IRequest<BusinessTripDto?>;