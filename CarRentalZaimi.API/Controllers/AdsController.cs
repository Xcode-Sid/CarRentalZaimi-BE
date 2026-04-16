using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Ads.Commands.CreateAds;
using CarRentalZaimi.Application.Features.Ads.Commands.DeleteAds;
using CarRentalZaimi.Application.Features.Ads.Commands.UpdateAds;
using CarRentalZaimi.Application.Features.Ads.Queries.GetAllAds;
using CarRentalZaimi.Application.Features.Ads.Queries.GetAllPagedAds;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class AdsController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateAds))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAds([FromBody] CreateAdsCommand command)
    {
        return await SendCommand(command, SuccessMessages.Ads.AdsCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateAds))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAds(
       [FromRoute] string id,
       [FromBody] UpdateAdsCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.Ads.AdsUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteAds))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAds([FromRoute] string id)
    {
        var command = new DeleteAdsCommand { Id = id };
        return await SendCommand(command, SuccessMessages.Ads.AdsDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllAds))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAds()
    {
        return await SendQuery(new GetAllAdsQuery(), null, StatusCodes.Status404NotFound);
    }

    [HttpGet("getPagedAds", Name = nameof(GetAllPagedAds))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<PagedResponse<AdsDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedAds([FromQuery] GetAllPagedAdsQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}


