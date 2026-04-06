using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Queries;
using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarCategoryController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarCategory))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarCategory([FromBody] CreateCarCategoryCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarCategory.CarCategoryCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarCategory))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarCategory(
       [FromRoute] string id,
       [FromBody] UpdateCarCategoryCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarCategory.CarCategoryUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarCategory))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarCategory([FromRoute] string id)
    {
        var command = new DeleteCarCategoryCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarCategory.CarCategoryDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCategories))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCategories()
    {
        return await SendQuery(new GetAllCarCategoryQuery(), null, StatusCodes.Status404NotFound);
    }
}

