using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;
using CarRentalZaimi.Application.Features.CarCategory.Queries;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarCategoryService
{
    Task<Result<CarCategoryDto>> CreateAsync(CreateCarCategoryCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarCategoryDto>> UpdateAsync(UpdateCarCategoryCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarCategoryCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarCategoryDto>>> GetAllAsync(GetAllCarCategoryQuery request, CancellationToken cancellationToken = default);
}
