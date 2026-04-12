using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;
using CarRentalZaimi.Application.Features.Partner.Commands.CreatePartner;
using CarRentalZaimi.Application.Features.Partner.Commands.DeletePartner;
using CarRentalZaimi.Application.Features.Partner.Commands.UpdatePartner;
using CarRentalZaimi.Application.Features.Partner.Queries.GetAllPagedPartners;
using CarRentalZaimi.Application.Features.Partner.Queries.GetAllPartners;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class PartnerController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreatePartner))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePartner([FromBody] CreatePartnerCommand command)
    {
        return await SendCommand(command, SuccessMessages.Partner.PartnerrCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdatePartner))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePartner(
       [FromRoute] string id,
       [FromBody] UpdatePartnerCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.Partner.PartnerUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeletePartner))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePartner([FromRoute] string id)
    {
        var command = new DeletePartnerCommand { Id = id };
        return await SendCommand(command, SuccessMessages.Partner.PartnerDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllPartners))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPartners()
    {
        return await SendQuery(new GetAllPartnersQuery(), null, StatusCodes.Status404NotFound);
    }

    [HttpGet("getPaged", Name = nameof(GetAllPagedPartners))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<PagedResponse<PartnerDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllPagedPartners([FromQuery] GetAllPagedPartnersQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}
