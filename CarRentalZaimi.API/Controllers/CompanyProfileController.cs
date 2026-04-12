using CarRentalZaimi.API.Controllers.Base;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.Features.CompanyProfile.Commands.AddCompanyProfileData;
using CarRentalZaimi.Application.Features.CompanyProfile.Queries.GetCompanyProfileData;
using CarRentalZaimi.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Controllers;

public class CompanyProfileController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [HttpPost("addCompanyProfileData")]
    [Authorize(SystemPolicies.Admin)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCompanyProfileData([FromBody] AddCompanyProfileDataCommand command)
    {
        return await SendCommand(command, SuccessMessages.Car.CarCreated);
    }

    [HttpGet("get", Name = nameof(GetCompanyProfileData))]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCompanyProfileData()
    {
        
        return await SendQuery(new GetCompanyProfileDataQuery(), SuccessMessages.BookingRequest.BookingRequestCanceled);
    }
}
