using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarTransmissionController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarTransmission))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarTransmission([FromBody] CreateCarTransmissionCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarTransmission.CarTransmissionCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarTransmission))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarTransmission(
       [FromRoute] string id,
       [FromBody] UpdateCarTransmissionCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarTransmission.CarTransmissionUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarTransmission))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarTransmission([FromRoute] string id)
    {
        var command = new DeleteCarTransmissionCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarTransmission.CarTransmissionDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarTransmission))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarTransmission()
    {
        return await SendQuery(new GetAllCarTransmissionQuery(), null, StatusCodes.Status404NotFound);
    }
}

