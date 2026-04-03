using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.Authentication.Command.Facebook;
using CarRentalZaimi.Application.Features.Authentication.Command.Google;
using CarRentalZaimi.Application.Features.Authentication.Command.Login;
using CarRentalZaimi.Application.Features.Authentication.Command.Microsoft;
using CarRentalZaimi.Application.Features.Authentication.Command.Register;
using CarRentalZaimi.Application.Features.Authentication.Command.Yahoo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class AuthenticationController(IMediator _mediator) : ApiControllerBase(_mediator)
{

    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        return await SendCommand(command);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return await SendCommand(command);
    }

    [HttpPost("google-verify")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyGoogleAuth([FromBody] AuthenticateWithGoogleCommand command)
    {
        return await SendCommand(command);
    }

    [HttpPost("facebook-verify")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyFacebookAuth([FromBody] AuthenticateWithFacebookCommand command)
    {
        return await SendCommand(command);
    }

    [HttpPost("microsoft-verify")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyMicrosoftAuth([FromBody] AuthenticateWithMicrosoftCommand command)
    {
        return await SendCommand(command);
    }

    [HttpPost("yahoo-verify")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyYahooAuth([FromBody] AuthenticateWithYahooCommand command)
    {
        return await SendCommand(command);
    }
}
