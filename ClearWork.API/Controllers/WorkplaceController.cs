using System.ComponentModel.DataAnnotations;
using ClearWork.Application.Workplaces.Commands.CreateUserWorkplace;
using ClearWork.Application.Workplaces.Commands.UpdateUserWorkplace;
using ClearWork.Application.Workplaces.Dtos;
using ClearWork.Application.Workplaces.Queries.GetUserWorkplace;
using ClearWork.Application.Workplaces.Queries.GetUserWorkplaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces")]
[Authorize]
public class WorkplaceController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkplaceDto>>> GetUserWorkplaces()
    {
        var userWorkplaces = await mediator.Send(new GetUserWorkplacesQuery());
        return Ok(userWorkplaces);
    }
    
    [HttpGet("{workplaceId:int}")]
    public async Task<ActionResult<WorkplaceDto?>> GetUserWorkplace([FromRoute] int workplaceId)
    {
        var userWorkplace = await mediator.Send(new GetUserWorkplaceQuery(workplaceId));
        return Ok(userWorkplace);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserWorkplace([FromBody, Required] CreateUserWorkplaceCommand command)
    {
        var workplaceId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetUserWorkplace), new { workplaceId }, null);
    }

    [HttpPatch("update/{workplaceId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserWorkplace([FromRoute] int workplaceId, [FromBody, Required] UpdateUserWorkplaceCommand command)
    {
        command.Id = workplaceId;
        await mediator.Send(command);
        return NoContent();
    }
}