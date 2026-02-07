using System.ComponentModel.DataAnnotations;
using ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractLeaveEntry;
using ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractWorkEntry;
using ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractLeaveEntry;
using ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractWorkEntry;
using ClearWork.Application.TimeEntries.Dtos;
using ClearWork.Application.TimeEntries.Queries.GetEmploymentContractTimeEntries;
using ClearWork.Application.TimeEntries.Queries.GetEmploymentContractTimeEntry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts/{contractId:int}/timeEntries")]
[Authorize]
public class TimeEntryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetEmploymentContractTimeEntries
        ([FromRoute] int workplaceId, [FromRoute] int contractId)
    {
        var employmentContractTimeEntries = 
            await mediator.Send(new GetEmploymentContractTimeEntriesQuery(workplaceId, contractId));
        return Ok(employmentContractTimeEntries);
    }
    
    [HttpGet("{timeEntryId:int}")]
    public async Task<ActionResult<TimeEntryDto>> GetEmploymentContractTimeEntry
        ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int timeEntryId)
    {
        var employmentContractTimeEntry =
            await mediator.Send(new GetEmploymentContractTimeEntryQuery(workplaceId, contractId, timeEntryId));
        return Ok(employmentContractTimeEntry);
    }
    
    [HttpPost("createWorkEntry")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEmploymentContractWorkEntry
        ([FromRoute] int workplaceId, [FromRoute] int contractId, 
            [FromBody, Required] CreateEmploymentContractWorkEntryCommand command)
    {
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        
        var timeEntryId = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetEmploymentContractTimeEntry), 
            new { workplaceId, contractId, timeEntryId }, null);
    }
    
    [HttpPost("createLeaveEntry")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEmploymentContractLeaveEntry
        ([FromRoute] int workplaceId, [FromRoute] int contractId,
            [FromBody, Required] CreateEmploymentContractLeaveEntryCommand command)
    {
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        
        var timeEntryId = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetEmploymentContractTimeEntry),
            new { workplaceId, contractId, timeEntryId }, null);
    }
    
    [HttpPatch("updateWorkEntry/{workEntryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmploymentContractWorkEntry
        ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int workEntryId,
            [FromBody, Required] UpdateEmploymentContractWorkEntryCommand command)
    {
        command.Id = workEntryId;
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpPatch("updateLeaveEntry/{leaveEntryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmploymentContractLeaveEntry
        ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int leaveEntryId,
            [FromBody, Required] UpdateEmploymentContractLeaveEntryCommand command)
    {
        command.Id = leaveEntryId;
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        
        await mediator.Send(command);
        return NoContent();
    }
}