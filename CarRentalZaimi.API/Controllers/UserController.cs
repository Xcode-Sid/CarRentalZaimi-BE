using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.Users.Commands.AddPhoneNumber;
using CarRentalZaimi.Application.Features.Users.Commands.UpdateUser;
using CarRentalZaimi.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class UserController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpGet("user/{userId}", Name = nameof(GetUserById))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(string userId)
    {
        return await SendQuery(new GetUserByIdQuery { UserId = userId }, null, StatusCodes.Status404NotFound);
    }


    [HttpPost("user/{userId}", Name = nameof(UpdateUserProfile))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserCommand command)
    {
        return await SendCommandCreated(command, nameof(GetUserById), new { userId = command.UserId }, SuccessMessages.User.UserProfileUpdated);
    }

    [HttpPost("user/{userId}/phone", Name = nameof(AddPhoneNumber))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPhoneNumber(string userId, string phoneNumber)
    {
        var command = new AddPhoneNumberCommand
        {
            UserId = userId,
            PhoneNumber = phoneNumber,
        };
        return await SendCommandCreated(command, nameof(GetUserById), new { userId }, SuccessMessages.User.UserProfileUpdated);
    }
}
