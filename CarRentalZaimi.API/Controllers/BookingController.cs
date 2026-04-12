using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.CreateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.AcceptBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.CreateBookingRequest;
using CarRentalZaimi.Application.Features.BookingRequest.Commands.RefuseBooking;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllBookings;
using CarRentalZaimi.Application.Features.BookingRequest.Queries.GetAllUserBookings;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarRentalZaimi.API.Controllers;

public class BookingController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateBookingRequest))]
    [Authorize(SystemPolicies.User)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBookingRequest([FromBody] CreateBookingRequestCommand command)
    {
        return await SendCommand(command, SuccessMessages.BookingRequest.BookingRequestCreated);
    }

    [HttpPut("refuse/{id}", Name = nameof(RefuseBookingRequest))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefuseBookingRequest([FromRoute] string id, [FromBody] RefuseBookingCommand command)
    {
        return await SendCommand(command, SuccessMessages.BookingRequest.BookingRequestRefused);
    }

    [HttpPut("accept/{id}", Name = nameof(AcceptBookingRequest))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AcceptBookingRequest([FromRoute] string id)
    {
        var command = new AcceptBookingCommand() { BookingId = id };
        return await SendCommand(command, SuccessMessages.BookingRequest.BookingRequestAccepted);
    }

    [HttpPut("cancel/{id}", Name = nameof(CancelBooking))]
    [Authorize(SystemPolicies.User)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelBooking(
      [FromRoute] string id, [FromBody] CancelBookingCommand command)
    {
        return await SendCommand(command, SuccessMessages.BookingRequest.BookingRequestCanceled);
    }

    [HttpGet("getAll", Name = nameof(GetAllBookings))]
    [ProducesResponseType(typeof(Result<PagedResponse<BookingDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBookings([FromQuery] GetAllBookingsQuery query)
    {
        var res = await SendCommand(query);
        return res;
    }


    [HttpGet("user/{id}", Name = nameof(GetAllUserBookings))]
    [ProducesResponseType(typeof(Result<PagedResponse<BookingDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUserBookings([FromRoute]  string id)
    {
        var query = new GetAllUserBookingsQuery() { UserId = id };
        var res = await SendCommand(query);
        return res;
    }
}
