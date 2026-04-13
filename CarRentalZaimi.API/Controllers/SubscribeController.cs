using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Subscribe.Commands.CreateSubscription;
using CarRentalZaimi.Application.Features.Subscribe.Commands.RemoveSubscription;
using CarRentalZaimi.Application.Features.Subscribe.Queries.GetAllPagedSubscriptions;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class SubscribeController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost("subscribe")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Subscribe([FromBody] CreateSubscriptionCommand command)
    {
        return await SendCommand(command, SuccessMessages.Subscribe.SubscriptionCreated);
    }

    [HttpPost("unsubscribe")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Unsubscribe([FromBody] RemoveSubscriptionCommand command)
    {
        return await SendCommand(command, SuccessMessages.Subscribe.SubscriptionRemoved);
    }

    [HttpGet("getPagedSubscription", Name = nameof(GetAllPagedSubscriptions))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<PagedResponse<SubscribeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedSubscriptions([FromQuery] GetAllPagedSubscriptionsQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }
}
