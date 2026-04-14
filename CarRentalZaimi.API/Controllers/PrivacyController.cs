using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Privacy.Commands.CreatePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Commands.DeletePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Commands.UpdatePrivacy;
using CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPagedPrivacies;
using CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPrivacies;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class PrivacyController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreatePrivacy))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePrivacy([FromBody] CreatePrivacyCommand command)
    {
        return await SendCommand(command, SuccessMessages.Privacy.PrivacyCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdatePrivacy))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePrivacy(
       [FromRoute] string id,
       [FromBody] UpdatePrivacyCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.Privacy.PrivacyUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeletePrivacy))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePrivacy([FromRoute] string id)
    {
        var command = new DeletePrivacyCommand { Id = id };
        return await SendCommand(command, SuccessMessages.Privacy.PrivacyDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllPrivacies))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPrivacies()
    {
        return await SendQuery(new GetAllPrivaciesQuery(), null, StatusCodes.Status404NotFound);
    }

    [HttpGet("getPagedPrivacies", Name = nameof(GetAllPagedPrivacies))]
    [ProducesResponseType(typeof(Result<PagedResponse<PartnerDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedPrivacies([FromQuery] GetAllPagedPrivaciesQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}


