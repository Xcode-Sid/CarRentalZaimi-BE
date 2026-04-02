using CarRentalZaimi.API.Exctention;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalZaimi.API.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    protected (string? UserId, IActionResult? UnauthorizedResult) GetCurrentUserIdOrUnauthorized()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return (null, Unauthorized(ApiResponse.FailureResponse(ApiResponseMessages.UserNotAuthenticated)));
        return (userId, null);
    }

    protected async Task<IActionResult> SendQuery<TResponse>(IRequest<Result<TResponse>> query)
    {
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    protected async Task<IActionResult> SendQuery<TResponse>(
        IRequest<Result<TResponse>> query,
        string? successMessage = null,
        int failureStatusCode = StatusCodes.Status400BadRequest)
    {
        var result = await _mediator.Send(query);
        if (!result.IsSuccessful)
            return StatusCode(
                failureStatusCode,
                ApiResponse<TResponse>.FailureResponse(result.ErrorMessage ?? ApiResponseMessages.InvalidRequest));

        return Ok(ApiResponse<TResponse>.SuccessResponse(result.Data!, successMessage));
    }

    protected async Task<IActionResult> SendCommand<TResponse>(IRequest<Result<TResponse>> command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    protected async Task<IActionResult> SendCommand<TResponse>(
        IRequest<Result<TResponse>> command,
        string? successMessage,
        int failureStatusCode = StatusCodes.Status400BadRequest)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
            return StatusCode(
                failureStatusCode,
                ApiResponse<TResponse>.FailureResponse(result.ErrorMessage ?? ApiResponseMessages.InvalidRequest));

        return Ok(ApiResponse<TResponse>.SuccessResponse(result.Data!, successMessage ?? SuccessMessages.General.OperationCompleted));
    }

    protected async Task<IActionResult> SendCommand(
        IRequest<Result> command,
        string? successMessage = null,
        int failureStatusCode = StatusCodes.Status400BadRequest)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
            return StatusCode(
                failureStatusCode,
                ApiResponse.FailureResponse(result.ErrorMessage ?? ApiResponseMessages.InvalidRequest));

        return Ok(ApiResponse.SuccessResponse(successMessage ?? SuccessMessages.General.OperationCompleted));
    }

    protected async Task<IActionResult> SendCommandCreated<TResponse>(
        IRequest<Result<TResponse>> command,
        string routeName,
        object routeValues,
        string? successMessage = null)
    {
        var result = await _mediator.Send(command);
        return result.ToCreatedResult(routeName, routeValues, successMessage);
    }

    protected async Task<IActionResult> SendCommandNoContent(IRequest<Result> command)
    {
        var result = await _mediator.Send(command);
        return result.ToNoContentResult();
    }

    protected async Task<IActionResult> SendPaginatedQuery<TDto>(
        IRequest<Result<List<TDto>>> query,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        var result = await _mediator.Send(query);
        if (!result.IsSuccessful)
            return result.ToActionResult();

        return result.Data!.ToPaginatedResult(totalCount, pageNumber, pageSize);
    }
}
