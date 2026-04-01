using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Features.Authentication.Command.Facebook;
using CarRentalZaimi.Application.Features.Authentication.Command.Google;
using CarRentalZaimi.Application.Features.Authentication.Command.Microsoft;
using CarRentalZaimi.Application.Features.Authentication.Command.Register;
using CarRentalZaimi.Application.Features.Authentication.Command.Yahoo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CarRentalZaimi.API.Controllers;

public class AuthenticationController(IMediator _mediator) : ApiControllerBase
{

    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return FromResult(result, StatusCodes.Status201Created);
    }

    [HttpPost("google-verify")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyGoogleAuth([FromBody] AuthenticateWithGoogleCommand command)
    {
        var result = await _mediator.Send(command);
        return FromResult(result, StatusCodes.Status201Created);
    }

    [HttpPost("facebook-verify")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyFacebookAuth([FromBody] AuthenticateWithFacebookCommand command)
    {
        var result = await _mediator.Send(command);
        return FromResult(result, StatusCodes.Status201Created);
    }

    [HttpPost("microsoft-verify")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyMicrosoftAuth([FromBody] AuthenticateWithMicrosoftCommand command)
    {
        var result = await _mediator.Send(command);
        return FromResult(result, StatusCodes.Status201Created);
    }

    [HttpPost("yahoo-verify")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyYahooAuth([FromBody] AuthenticateWithYahooCommand command)
    {
        var result = await _mediator.Send(command);
        return FromResult(result, StatusCodes.Status201Created);
    }
}
