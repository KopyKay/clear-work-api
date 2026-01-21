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
}