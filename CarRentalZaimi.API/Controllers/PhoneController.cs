using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.Phone.Commands.ConfirmPhone;
using CarRentalZaimi.Application.Features.Phone.Commands.SendVerificationCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class PhoneController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost("send-verification-code")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeCommand command)
    {
        return await SendCommand(command);
    }

    [HttpPost("confirm-phone")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmPhoneCommand command)
    {
        return await SendCommand(command);
    }
}
