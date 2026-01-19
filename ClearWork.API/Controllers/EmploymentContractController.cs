using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts")]
[Authorize]
public class EmploymentContractController(IMediator mediator) : ControllerBase
{
    
}