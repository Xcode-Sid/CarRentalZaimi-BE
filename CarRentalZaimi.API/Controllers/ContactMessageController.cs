using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.ContactMessage.Commands.CreateContactMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class ContactMessageController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost("contact")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Subscribe([FromBody] CreateContactMessageCommand command)
    {
        return await SendCommand(command, SuccessMessages.ContactMessage.ContactMessageCreated);
    }

}
