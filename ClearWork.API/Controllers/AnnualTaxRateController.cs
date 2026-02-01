using System.ComponentModel.DataAnnotations;
using ClearWork.Application.AnnualTaxRates.Commands.CreateUserAnnualTaxRate;
using ClearWork.Application.AnnualTaxRates.Commands.UpdateUserAnnualTaxRate;
using ClearWork.Application.AnnualTaxRates.Dtos;
using ClearWork.Application.AnnualTaxRates.Queries.GetUserAnnualTaxRate;
using ClearWork.Application.AnnualTaxRates.Queries.GetUserAnnualTaxRates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/annualTaxRates")]
[Authorize]
public class AnnualTaxRateController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnnualTaxRateDto>>> GetUserAnnualTaxRates()
    {
        var userAnnualTaxRates = await mediator.Send(new GetUserAnnualTaxRatesQuery());
        return Ok(userAnnualTaxRates);
    }
    
    [HttpGet("{year:int}")]
    public async Task<ActionResult<AnnualTaxRateDto?>> GetUserAnnualTaxRate([FromRoute] int year)
    {
        var userAnnualTaxRate = await mediator.Send(new GetUserAnnualTaxRateQuery(year));
        return Ok(userAnnualTaxRate);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAnnualTaxRate([FromBody, Required] CreateUserAnnualTaxRateCommand command)
    {
        var year = await mediator.Send(command);
        return CreatedAtAction(nameof(GetUserAnnualTaxRate), new { year }, null);
    }

    [HttpPatch("update/{year:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserAnnualTaxRate([FromRoute] int year, [FromBody, Required] UpdateUserAnnualTaxRateCommand command)
    {
        command.Year = year;
        await mediator.Send(command);
        return NoContent();
    }
}