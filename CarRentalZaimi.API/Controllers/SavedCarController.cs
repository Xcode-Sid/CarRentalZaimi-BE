using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.SavedCar.Command.SaveCar;
using CarRentalZaimi.Application.Features.SavedCar.Queries;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;



[Authorize(SystemPolicies.User)]
public class SavedCarController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost("save")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveCar([FromBody] SaveCarCommand command)
    {
        return await SendCommand(command, SuccessMessages.SavedCar.CarSaved);
    }

    [HttpGet("getAll-savedCars")]
    [ProducesResponseType(typeof(Result<PagedResponse<SavedCarDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSavedCar([FromQuery] GetAllSavedCarsQuery query)
    {
        var res = await SendCommand(query);
        return res;
    }
}
