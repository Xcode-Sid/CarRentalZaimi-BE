using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Queries;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarCategoryService
{
    Task<ApiResponse<CarCategoryDto>> CreateAsync(CreateCarCategoryCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarCategoryDto>> UpdateAsync(UpdateCarCategoryCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarCategoryCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarCategoryDto>>> GetAllAsync(GetAllCarCategoryQuery request, CancellationToken cancellationToken = default);
}
