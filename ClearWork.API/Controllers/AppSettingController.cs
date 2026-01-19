using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/appSettings")]
[Authorize]
public class AppSettingController(IMediator mediator) : ControllerBase
{
    
}