using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.DeleteStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Commands.UpdateStatePrefix;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries.GetAllPagedStatePrefixes;
using CarRentalZaimi.Application.Features.StatePrefixes.Queries.GetAllStatePrefixes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CarRentalZaimi.API.Controllers;

public class StatePrefixController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateStatePrefix))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStatePrefix([FromBody] CreateStatePrefixCommand command)
    {
        return await SendCommand(command, SuccessMessages.StatePrefix.StatePrefixCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateStatePrefix))]
    [EnableRateLimiting("strict")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatePrefix(
       [FromRoute] string id,
       [FromBody] UpdateStatePrefixCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.StatePrefix.StatePrefixUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteStatePrefix))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteStatePrefix([FromRoute] string id)
    {
        var command = new DeleteStatePrefixCommand { Id = id };
        return await SendCommand(command, SuccessMessages.StatePrefix.StatePrefixDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllStatePrefixes))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllStatePrefixes()
    {
        return await SendQuery(new GetAllStatePrefixesQuery(), null, StatusCodes.Status404NotFound);
    }



    [HttpGet("getAllPaged", Name = nameof(GetAllPagedStatePrefixes))]
    [ProducesResponseType(typeof(Result<PagedResponse<PartnerDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedStatePrefixes([FromQuery] GetAllPagedStatePrefixesQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}
