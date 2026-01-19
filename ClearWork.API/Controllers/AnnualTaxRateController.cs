using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/annualTaxRates")]
[Authorize]
public class AnnualTaxRateController(IMediator mediator) : ControllerBase
{
    
}