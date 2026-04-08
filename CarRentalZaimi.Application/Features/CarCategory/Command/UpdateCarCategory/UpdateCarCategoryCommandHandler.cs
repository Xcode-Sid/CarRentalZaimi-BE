using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;

internal class UpdateCarCategoryCommandHandler(ICarCategoryService _carCategoryService) : ICommandHandler<UpdateCarCategoryCommand, CarCategoryDto>
{
    public async Task<ApiResponse<CarCategoryDto>> Handle(UpdateCarCategoryCommand request, CancellationToken cancellationToken)
        => await _carCategoryService.UpdateAsync(request, cancellationToken);
}
