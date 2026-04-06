using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarCompanyNameController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarCompanyName))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarCompanyName([FromBody] CreateCarCompanyNameCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarCompanyName.CarCompanyNameCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarCompanyName))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarCompanyName(
       [FromRoute] string id,
       [FromBody] UpdateCarCompanyNameCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarCompanyName.CarCompanyNameUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarCompanyName))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarCompanyName([FromRoute] string id)
    {
        var command = new DeleteCarCompanyNameCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarCompanyName.CarCompanyNameDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarCompanyName))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarCompanyName()
    {
        return await SendQuery(new GetAllCarCompanyNameQuery(), null, StatusCodes.Status404NotFound);
    }
}
