using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Cars.Commands.CreateCar;
using CarRentalZaimi.Application.Features.Cars.Commands.DeleteCar;
using CarRentalZaimi.Application.Features.Cars.Commands.UpdateCar;
using CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;
using CarRentalZaimi.Application.Features.Cars.Queries.GetCarById;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarsController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarCommand command)
    {
        return await SendCommand(command, SuccessMessages.Car.CarCreated);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<CarDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarById([FromRoute] string id)
    {
        return await SendCommand(new GetCarByIdQuery(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResponse<CarDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCars([FromQuery] GetAllCarsQuery query)
    {
        return await SendCommand(query);
    }

    [HttpPut("{id}")]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCar([FromRoute] string id, [FromBody] UpdateCarCommand command)
    {
        var updatedCommand = command with { CarId = id };
        return await SendCommand(updatedCommand, SuccessMessages.Car.CarUpdated);
    }

    [HttpDelete("{id}")]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCar([FromRoute] string id)
    {
        return await SendCommand(new DeleteCarCommand(id));
    }
}
