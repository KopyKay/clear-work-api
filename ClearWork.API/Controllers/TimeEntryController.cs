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
}