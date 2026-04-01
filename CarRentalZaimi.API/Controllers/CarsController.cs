using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Features.Cars.Commands.RegisterCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarsController(IMediator _mediator) : ApiControllerBase
{
    [HttpPost]
    //TODO later
    //[Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Register(
         [FromBody] RegisterCarCommand command,
         CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return FromResult(result, StatusCodes.Status201Created);
    }
}
