using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarInteriorColorController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarInteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarInteriorColor([FromBody] CreateCarInteriorColorCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarInteriorColor.CarInteriorColorCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarInteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarInteriorColor(
       [FromRoute] string id,
       [FromBody] UpdateCarInteriorColorCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarInteriorColor.CarInteriorColorUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarInteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarInteriorColor([FromRoute] string id)
    {
        var command = new DeleteCarInteriorColorCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarInteriorColor.CarInteriorColorDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarInteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarInteriorColor()
    {
        return await SendQuery(new GetAllCarInteriorColorQuery(), null, StatusCodes.Status404NotFound);
    }
}

