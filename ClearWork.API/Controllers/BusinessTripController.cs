using System.ComponentModel.DataAnnotations;
using ClearWork.Application.BusinessTrips.Commands.CreateEmploymentContractBusinessTrip;
using ClearWork.Application.BusinessTrips.Commands.UpdateEmploymentContractBusinessTrip;
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
    
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEmploymentContractBusinessTrip
    ([FromRoute] int workplaceId, [FromRoute] int contractId,
        [FromBody, Required] CreateEmploymentContractBusinessTripCommand command)
    {
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        
        var businessTripId = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetEmploymentContractBusinessTrip), 
            new { workplaceId, contractId, businessTripId }, null);
    }

    [HttpPatch("update/{businessTripId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmploymentContractBusinessTrip
    ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int businessTripId,
        [FromBody, Required] UpdateEmploymentContractBusinessTripCommand command)
    {
        command.Id = businessTripId;
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        
        await mediator.Send(command);
        return NoContent();
    }
}