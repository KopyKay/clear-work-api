using ClearWork.Application.BusinessTrips.Dtos;
using ClearWork.Application.BusinessTrips.Queries.GetEmploymentContractBusinessTrip;
using ClearWork.Application.BusinessTrips.Queries.GetEmploymentContractBusinessTrips;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts/{contractId:int}/businessTrips")]
[Authorize]
public class BusinessTripController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessTripDto>>> GetEmploymentContractBusinessTrips
        ([FromRoute] int workplaceId, [FromRoute] int contractId)
    {
        var contractBusinessTrips = 
            await mediator.Send(new GetEmploymentContractBusinessTripsQuery(workplaceId, contractId));
        return Ok(contractBusinessTrips);
    }

    [HttpGet("{businessTripId:int}")]
    public async Task<ActionResult<BusinessTripDto?>> GetEmploymentContractBusinessTrip
        ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int businessTripId)
    {
        var contractBusinessTrip =
            await mediator.Send(new GetEmploymentContractBusinessTripQuery(workplaceId, contractId, businessTripId));
        return Ok(contractBusinessTrip);
    }
}