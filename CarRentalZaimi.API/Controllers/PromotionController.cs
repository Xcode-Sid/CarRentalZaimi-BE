using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.UpdatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetAllPromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetPromotionByCarId;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class PromotionController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreatePromotion))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionCommand command)
    {
        return await SendCommand(command, SuccessMessages.Promotion.PromotionCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdatePromotion))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePromotion(
       [FromRoute] string id,
       [FromBody] UpdatePromotionCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.Promotion.PromotionUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeletePromotion))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePromotion([FromRoute] string id)
    {
        var command = new DeletePromotionCommand { Id = id };
        return await SendCommand(command, SuccessMessages.Promotion.PromotionDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllPromotion))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPromotion()
    {
        return await SendQuery(new GetAllPromotionQuery(), null, StatusCodes.Status404NotFound);
    }

    [HttpGet("getPromotionForCar", Name = nameof(GetPromotion))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPromotion([FromQuery] GetPromotionByCarIdQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}
