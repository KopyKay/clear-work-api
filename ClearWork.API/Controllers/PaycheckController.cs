using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts/{employmentContractId:int}/paychecks")]
[Authorize]
public class PaycheckController(IMediator mediator) : ControllerBase
{
    
}