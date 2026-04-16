using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.CreateOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.DeleteOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.UpdateOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetCarCalendarData;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetOccupiedCarDays;
using CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class OccupiedCarDaysController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(Create))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOccupiedCarDaysCommand command)
    {
        return await SendCommand(command, SuccessMessages.OccupiedCarDays.OccupiedCarDaysCreated);
    }

    [HttpPut("{id}", Name = nameof(Update))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
       [FromRoute] string id,
       [FromBody] UpdateOccupiedCarDaysCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.OccupiedCarDays.OccupiedCarDaysUpdated);
    }

    [HttpDelete("{id}", Name = nameof(Delete))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var command = new DeleteOccupiedCarDaysCommand { Id = id };
        return await SendCommand(command, SuccessMessages.OccupiedCarDays.OccupiedCarDaysDeleted);
    }

    [HttpGet("car/occupied-days", Name = nameof(GetOccupiedDays))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<PagedResponse<OccupiedCarDaysDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOccupiedDays([FromQuery] GetOccupiedCarDaysQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }

    [HttpGet("get/calendarData", Name = nameof(GetCalendarData))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCalendarData([FromQuery] GetCarCalendarDataQuery query)
    {
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }

}



