using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.CreateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class AdditionalServiceController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost(Name = nameof(CreateAdditionalService))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAdditionalService([FromBody] CreateAdditionalServiceCommand command)
    {
        return await SendCommand(command, SuccessMessages.AditionalServices.AditionalServicesCreated);
    }

    [HttpPut("{id}", Name = nameof(UpdateAdditionalSService))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAdditionalSService(
       [FromRoute] string id,
       [FromBody] UpdateAdditionalServiceCommand command)
    {
        var updatedCommand = command with { Id = id };
        return await SendCommand(updatedCommand, SuccessMessages.AditionalServices.AditionalServicesUpdated);
    }

    [HttpDelete("{id}", Name = nameof(DeleteAdditionalService))]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAdditionalService([FromRoute] string id)
    {
        var command = new DeleteAdditionalServiceCommand { Id = id };
        return await SendCommand(command, SuccessMessages.AditionalServices.AditionalServicesDeleted);
    }

    [HttpGet("getAll", Name = nameof(GetAllAdditionalSServices))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAdditionalSServices()
    {
        return await SendQuery(new GetAllAdditionalServicesQuery(), null, StatusCodes.Status404NotFound);
    }
}
