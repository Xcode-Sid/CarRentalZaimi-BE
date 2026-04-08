using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalZaimi.API.Exctention;

public static class ControllerExtensions
{
    public static IActionResult ToPaginatedApiResponse<T>(
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
