using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarFuelController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarFuel))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarFuel([FromBody] CreateCarFuelCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarFuel.CarFuelCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarFuel))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarFuel(
       [FromRoute] string id,
       [FromBody] UpdateCarFuelCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarFuel.CarFuelUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarFuel))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarFuel([FromRoute] string id)
    {
        var command = new DeleteCarFuelCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarFuel.CarFuelDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarFuels))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarFuels()
    {
        return await SendQuery(new GetAllCarFuelsQuery(), null, StatusCodes.Status404NotFound);
    }
}
