using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CarCategoryController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateCarCategory))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCarCategory([FromBody] CreateCarCategoryCommand command)
    {
        return await SendCommand(command, SuccessMessages.CarCategory.CarCategoryCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateCarCategory))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarCategory(
       [FromRoute] string id,
       [FromBody] UpdateCarCategoryCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.CarCategory.CarCategoryUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCarCategory))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCarCategory([FromRoute] string id)
    {
        var command = new DeleteCarCategoryCommand { Id = id };
        return await SendCommand(command, SuccessMessages.CarCategory.CarCategoryDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllCategories))]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCategories()
    {
        return await SendQuery(new GetAllCarCategoryQuery(), null, StatusCodes.Status404NotFound);
    }
}

