using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarInterior.Queries.GetAllCarInteriorColor;
using CarRentalZaimi.Application.Features.CarReview.Commands.CreateCarReview;
using CarRentalZaimi.Application.Features.CarReview.Commands.DeleteCarReview;
using CarRentalZaimi.Application.Features.CarReview.Commands.UpdateCarReview;
using CarRentalZaimi.Application.Features.CarReview.Queries;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllCarReview;
using CarRentalZaimi.Application.Features.CarReview.Queries.GetAllPagedCarReview;
using CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarReviewController(IMediator _mediator) : ApiControllerBase(_mediator)
{

    [HttpPost("create")]
    [Authorize(SystemPolicies.User)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarReview([FromBody] CreateCarReviewCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarReview.CarReviewCreated);
    }

    [HttpPut("update/{id}")]
    [Authorize(SystemPolicies.User)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCarReview([FromRoute] string id, [FromBody] UpdateCarReviewCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarReview.CarReviewUpdated);
    }

    [HttpDelete("{id}")]
    [Authorize(SystemPolicies.User)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCarReview([FromRoute] string id)
    {
        var command = new DeleteCarReviewCommand { Id = id};
        return await SendCommand(command, SuccessMessages.CarReview.CarReviewDeleted);
    }

    [HttpGet("getAll/{carId}", Name = nameof(GetAllCarReview))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarReview([FromRoute] string carId)
    {
        var query = new GetAllCarReviewQuery() { CarId = carId };
        return await SendQuery(query, null, StatusCodes.Status404NotFound);
    }

    [HttpGet("getAllPaged")]
    [ProducesResponseType(typeof(Result<PagedResponse<CarDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPagedCarReviews([FromQuery] GetAllPagedCarReviewQuery query)
    {
        var res = await SendCommand(query);
        return res;
    }
}
