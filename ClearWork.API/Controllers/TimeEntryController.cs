using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts/{employmentContractId:int}/timeEntries")]
[Authorize]
public class TimeEntryController(IMediator mediator) : ControllerBase
{
    
}