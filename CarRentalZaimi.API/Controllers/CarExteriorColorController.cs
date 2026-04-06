using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Queries;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarExteriorColorController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarExteriorColor))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarExteriorColor([FromBody] CreateCarExteriorColorCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarExteriorColor.CarExteriorColorCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarExteriorColor))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarExteriorColor(
       [FromRoute] string id,
       [FromBody] UpdateCarExteriorColorCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarExteriorColor.CarExteriorColorUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarExteriorColor))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarExteriorColor([FromRoute] string id)
    {
        var command = new DeleteCarExteriorColorCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarExteriorColor.CarExteriorColorDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarExteriorColor))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarExteriorColor()
    {
        return await SendQuery(new GetAllCarExteriorColorQuery(), null, StatusCodes.Status404NotFound);
    }
}
