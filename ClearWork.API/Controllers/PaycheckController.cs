using System.ComponentModel.DataAnnotations;
using ClearWork.Application.Paychecks.Commands.CreateEmploymentContractPaycheck;
using ClearWork.Application.Paychecks.Commands.UpdateEmploymentContractPaycheck;
using ClearWork.Application.Paychecks.Dtos;
using ClearWork.Application.Paychecks.Queries.GetEmploymentContractPaycheck;
using ClearWork.Application.Paychecks.Queries.GetEmploymentContractPaychecks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts/{contractId:int}/paychecks")]
[Authorize]
public class PaycheckController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaycheckDto>>> GetEmploymentContractPaychecks
        ([FromRoute] int workplaceId, [FromRoute] int contractId)
    {
        var contractPaychecks = 
            await mediator.Send(new GetEmploymentContractPaychecksQuery(workplaceId, contractId));
        return Ok(contractPaychecks);
    }
    
    [HttpGet("{paycheckId:int}")]
    public async Task<ActionResult<PaycheckDto?>> GetEmploymentContractPaycheck
        ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int paycheckId)
    {
        var contractPaycheck = 
            await mediator.Send(new GetEmploymentContractPaycheckQuery(workplaceId, contractId, paycheckId));
        return Ok(contractPaycheck);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEmploymentContractPaycheck
        ([FromRoute] int workplaceId, [FromRoute] int contractId, 
            [FromBody, Required] CreateEmploymentContractPaycheckCommand command)
    {
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        var paycheckId = await mediator.Send(command);
        
        return 
            CreatedAtAction(nameof(GetEmploymentContractPaycheck), 
                new { workplaceId, contractId, paycheckId }, null);
    }
    
    [HttpPatch("update/{paycheckId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmploymentContractPaycheck
        ([FromRoute] int workplaceId, [FromRoute] int contractId, [FromRoute] int paycheckId,
            [FromBody, Required] UpdateEmploymentContractPaycheckCommand command)
    {
        command.Id = paycheckId;
        command.WorkplaceId = workplaceId;
        command.ContractId = contractId;
        await mediator.Send(command);
        return NoContent();
    }
}