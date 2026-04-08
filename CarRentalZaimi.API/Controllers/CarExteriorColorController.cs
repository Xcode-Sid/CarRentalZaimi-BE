using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Commands.UpdateCarExteriorColor;
using CarRentalZaimi.Application.Features.CarExteriorColor.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarExteriorColorController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarExteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarExteriorColor([FromBody] CreateCarExteriorColorCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarExteriorColor.CarExteriorColorCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarExteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarExteriorColor(
       [FromRoute] string id,
       [FromBody] UpdateCarExteriorColorCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarExteriorColor.CarExteriorColorUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarExteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarExteriorColor([FromRoute] string id)
    {
        var command = new DeleteCarExteriorColorCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarExteriorColor.CarExteriorColorDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarExteriorColor))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarExteriorColor()
    {
        return await SendQuery(new GetAllCarExteriorColorQuery(), null, StatusCodes.Status404NotFound);
    }
}
