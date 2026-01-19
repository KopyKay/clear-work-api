using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces")]
[Authorize]
public class WorkplaceController(IMediator mediator) : ControllerBase
{
    
}