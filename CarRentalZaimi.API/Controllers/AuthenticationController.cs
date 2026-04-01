using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Features.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MySqlX.XDevAPI.Relational;

namespace CarRentalZaimi.API.Controllers;

public class AuthenticationController(IMediator _mediator) : ApiControllerBase
{

    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return FromResult(result, StatusCodes.Status201Created);
    }
}
