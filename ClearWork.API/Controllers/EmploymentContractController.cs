using ClearWork.Application.EmploymentContracts.Dtos;
using ClearWork.Application.EmploymentContracts.Queries.GetWorkplaceEmploymentContract;
using ClearWork.Application.EmploymentContracts.Queries.GetWorkplaceEmploymentContracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts")]
[Authorize]
public class EmploymentContractController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmploymentContractDto>>> GetWorkplaceEmploymentContracts
        ([FromRoute] int workplaceId)
    {
        var workplaceEmploymentContracts = 
            await mediator.Send(new GetWorkplaceEmploymentContractsQuery(workplaceId));
        
        return Ok(workplaceEmploymentContracts);
    }
    
    [HttpGet("{contractId:int}")]
    public async Task<ActionResult<EmploymentContractDto?>> GetWorkplaceEmploymentContract
        ([FromRoute] int workplaceId, [FromRoute] int contractId)
    {
        var workplaceEmploymentContract = 
            await mediator.Send(new GetWorkplaceEmploymentContractQuery(workplaceId, contractId));
        return Ok(workplaceEmploymentContract);
    }
}