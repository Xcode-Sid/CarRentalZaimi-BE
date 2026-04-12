using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Terms.Commands.CreateTerm;
using CarRentalZaimi.Application.Features.Terms.Commands.DeleteTerm;
using CarRentalZaimi.Application.Features.Terms.Commands.UpdateTerm;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllTerms;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class TermsController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateTerm))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTerm([FromBody] CreateTermCommand command)
    {
        return await SendCommand(command, SuccessMessages.Term.TermCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateTerm))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTerm(
       [FromRoute] string id,
       [FromBody] UpdateTermCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.Term.TermUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteTerm))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTerm([FromRoute] string id)
    {
        var command = new DeleteTermCommand { Id = id };
        return await SendCommand(command, SuccessMessages.Term.TermDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllTerms))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTerms()
    {
        return await SendQuery(new GetAllTermsQuery(), null, StatusCodes.Status404NotFound);
    }

    [HttpGet("getPagedTerms", Name = nameof(GetAllPagedTerms))]
    [ProducesResponseType(typeof(Result<PagedResponse<PartnerDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedTerms([FromQuery] GetAllPagedTermsQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}

