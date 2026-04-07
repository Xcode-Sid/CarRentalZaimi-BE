using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.UpdateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Queries.GetAllCarCompanyModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarCompanyModelController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarCompanyModel))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarCompanyModel([FromBody] CreateCarCompanyModelCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarCompanyModel.CarCompanyModelCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarCompanyModel))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarCompanyModel(
       [FromRoute] string id,
       [FromBody] UpdateCarCompanyModelCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarCompanyModel.CarCompanyModelUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarCompanyModel))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarCompanyModel([FromRoute] string id)
    {
        var command = new DeleteCarCompanyModelCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarCompanyModel.CarCompanyModelDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCarCompanyModel))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCarCompanyModel()
    {
        return await SendQuery(new GetAllCarCompanyModelQuery(), null, StatusCodes.Status404NotFound);
    }
}
