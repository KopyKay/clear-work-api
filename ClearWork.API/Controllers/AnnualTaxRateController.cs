using ClearWork.Application.AnnualTaxRates.Dtos;
using ClearWork.Application.AnnualTaxRates.Queries.GetAnnualTaxRate;
using ClearWork.Application.AnnualTaxRates.Queries.GetAnnualTaxRates;
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
        var userAnnualTaxRates = await mediator.Send(new GetAnnualTaxRatesQuery());
        return Ok(userAnnualTaxRates);
    }
    
    [HttpGet("{year:int}")]
    public async Task<ActionResult<AnnualTaxRateDto?>> GetUserAnnualTaxRate([FromRoute] int year)
    {
        var userAnnualTaxRate = await mediator.Send(new GetAnnualTaxRateQuery(year));
        return Ok(userAnnualTaxRate);
    }
}