using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
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

    protected async Task<IActionResult> SendQuery<TResponse>(IRequest<ApiResponse<TResponse>> query)
    {
        var response = await _mediator.Send(query);
        if (!response.IsSuccess)
            return BadRequest(response);
        return Ok(response);
    }

    protected async Task<IActionResult> SendQuery<TResponse>(
        IRequest<ApiResponse<TResponse>> query,
        string? successMessage = null,
        int failureStatusCode = StatusCodes.Status400BadRequest)
    {
        var result = await _mediator.Send(query);
        if (!result.IsSuccess)
            return StatusCode(
                failureStatusCode,
                ApiResponse<TResponse>.FailureResponse(result.ErrorResult ?? ApiResponseMessages.InvalidRequest));

        return Ok(ApiResponse<TResponse>.SuccessResponse(result.Data!, successMessage));
    }

    protected async Task<IActionResult> SendCommand<TResponse>(IRequest<ApiResponse<TResponse>> command)
    {
        var response = await _mediator.Send(command);
        if (!response.IsSuccess)
            return BadRequest(response);
        return Ok(response);
    }

    protected async Task<IActionResult> SendCommand<TResponse>(
        IRequest<ApiResponse<TResponse>> command,
        string? successMessage,
        int failureStatusCode = StatusCodes.Status400BadRequest)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return StatusCode(
                failureStatusCode,
                ApiResponse<TResponse>.FailureResponse(result.ErrorResult ?? ApiResponseMessages.InvalidRequest));

        return Ok(ApiResponse<TResponse>.SuccessResponse(result.Data!, successMessage ?? SuccessMessages.General.OperationCompleted));
    }

    protected async Task<IActionResult> SendCommand(
        IRequest<ApiResponse> command,
        string? successMessage = null,
        int failureStatusCode = StatusCodes.Status400BadRequest)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return StatusCode(
                failureStatusCode,
                ApiResponse.FailureResponse(result.ErrorResult ?? ApiResponseMessages.InvalidRequest));

        return Ok(ApiResponse.SuccessResponse(successMessage ?? SuccessMessages.General.OperationCompleted));
    }

    protected async Task<IActionResult> SendCommandCreated<TResponse>(
        IRequest<ApiResponse<TResponse>> command,
        string routeName,
        object routeValues,
        string? successMessage = null)
    {
        var response = await _mediator.Send(command);
        if (!response.IsSuccess)
            return BadRequest(response);
        response.Message ??= successMessage ?? SuccessMessages.General.ResourceCreated;
        return CreatedAtRoute(routeName, routeValues, response);
    }

    protected async Task<IActionResult> SendCommandNoContent(IRequest<ApiResponse> command)
    {
        var response = await _mediator.Send(command);
        if (!response.IsSuccess)
            return BadRequest(response);
        return NoContent();
    }

    protected async Task<IActionResult> SendPaginatedQuery<TDto>(
        IRequest<ApiResponse<List<TDto>>> query,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        var response = await _mediator.Send(query);
        if (!response.IsSuccess)
            return BadRequest(response);

        var pagedResponse = new PagedResponse<TDto>(
            response.Data!,
            totalCount,
            pageNumber,
            pageSize);

        return Ok(ApiResponse<PagedResponse<TDto>>.SuccessResponse(pagedResponse));
    }
}
