using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.UpdateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CarRentalZaimi.API.Controllers;

public class StatePrefixController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost(Name = nameof(CreateStatePrefix))]
    [ProducesResponseType(typeof(ApiResponse<StatePrefixDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStatePrefix([FromBody] CreateStatePrefixCommand command)
        => await SendCommand(command, SuccessMessages.StatePrefix.StatePrefixCreated);

    [HttpPut("{id}", Name = nameof(UpdateStatePrefix))]
    [EnableRateLimiting("strict")]
    [ProducesResponseType(typeof(ApiResponse<StatePrefixDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatePrefix(
        [FromRoute] string id,
        [FromBody] UpdateStatePrefixCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.StatePrefix.StatePrefixUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteStatePrefix))]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteStatePrefix([FromRoute] string id)
    {
        var command = new DeleteStatePrefixCommand { Id = id };
        return await SendCommand(command, SuccessMessages.StatePrefix.StatePrefixDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllStatePrefixes))]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<StatePrefixDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllStatePrefixes()
        => await SendQuery(new GetAllStatePrefixesQuery(), null, StatusCodes.Status404NotFound);
}
