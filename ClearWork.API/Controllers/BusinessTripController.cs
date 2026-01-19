using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearWork.API.Controllers;

[ApiController]
[Route("api/workplaces/{workplaceId:int}/employmentContracts/{employmentContractId:int}/businessTrips")]
[Authorize]
public class BusinessTripController(IMediator mediator) : ControllerBase
{
    
}