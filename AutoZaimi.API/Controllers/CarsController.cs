using AutoZaimi.Application.Features.Cars.Commands.RegisterCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoZaimi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    //TODO later
    //[Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Register(
         [FromBody] RegisterCarCommand command,
         CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result); //TODO later
    }
}
