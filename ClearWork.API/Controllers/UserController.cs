using System.ComponentModel.DataAnnotations;
using ClearWork.Application.Users.UpdateUserDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserDetails([FromBody, Required] UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}