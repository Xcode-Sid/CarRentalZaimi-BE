using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Messages;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Exctention;


public static class ControllerExtensions
{
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccessful)
            return new OkObjectResult(ApiResponse.SuccessResponse(SuccessMessages.General.OperationCompleted));

        return new BadRequestObjectResult(ApiResponse.FailureResponse(result.ErrorMessage));
    }

    public static IActionResult ToActionResult<T>(this Result<T> result, string? successMessage = null)
    {
        if (result.IsSuccessful)
            return new OkObjectResult(ApiResponse<T>.SuccessResponse(result.Data!, successMessage));

        return new BadRequestObjectResult(ApiResponse<T>.FailureResponse(result.ErrorMessage));
    }

    public static IActionResult ToCreatedResult<T>(
        this Result<T> result,
        string routeName,
        object routeValues,
        string? successMessage = null)
    {
        if (result.IsSuccessful)
            return new CreatedAtRouteResult(
                routeName,
                routeValues,
                ApiResponse<T>.SuccessResponse(result.Data!, successMessage ?? SuccessMessages.General.ResourceCreated));

        return new BadRequestObjectResult(ApiResponse<T>.FailureResponse(result.ErrorMessage));
    }

    public static IActionResult ToNoContentResult(this Result result)
    {
        if (result.IsSuccessful)
            return new NoContentResult();
        return new BadRequestObjectResult(ApiResponse.FailureResponse(result.ErrorMessage));
    }

    public static IActionResult ToPaginatedResult<T>(
        this IEnumerable<T> items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        var pagedResponse = new PagedResponse<T>(
            items.ToList(),
            totalCount,
            pageNumber,
            pageSize);

        return new OkObjectResult(ApiResponse<PagedResponse<T>>.SuccessResponse(pagedResponse));
    }
}


