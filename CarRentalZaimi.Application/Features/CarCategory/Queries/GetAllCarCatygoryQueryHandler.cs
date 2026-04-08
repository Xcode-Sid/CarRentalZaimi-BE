using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCategory.Queries;

internal class GetAllCarCatygoryQueryHandler(ICarCategoryService _carCategoryService) : IQueryHandler<GetAllCarCategoryQuery, IEnumerable<CarCategoryDto>>
{
    public async Task<ApiResponse<IEnumerable<CarCategoryDto>>> Handle(GetAllCarCategoryQuery request, CancellationToken cancellationToken)
        => await _carCategoryService.GetAllAsync(request, cancellationToken);
}