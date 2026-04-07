using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Features.Cars.Commands.RegisterCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarsController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost]
    //TODO later
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCarCommand command,
        CancellationToken ct)
        => await SendCommand(command);
}
