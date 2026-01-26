using System.ComponentModel.DataAnnotations;
using ClearWork.Application.AppSettings.Commands.CreateUserAppSettings;
using ClearWork.Application.AppSettings.Commands.UpdateUserAppSettings;
using ClearWork.Application.AppSettings.Dtos;
using ClearWork.Application.AppSettings.Queries.GetUserAppSettings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/appSettings")]
[Authorize]
public class AppSettingController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserAppSettings([FromBody, Required] CreateUserAppSettingsCommand command)
    {
        var userAppSettingsId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetUserAppSettings), new { userAppSettingsId }, null);
    }
    
    [HttpGet]
    public async Task<ActionResult<AppSettingDto?>> GetUserAppSettings()
    {
        var userAppSettings = await mediator.Send(new GetUserAppSettingsQuery());
        return Ok(userAppSettings);
    }

    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserAppSettings([FromBody, Required] UpdateUserAppSettingsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}