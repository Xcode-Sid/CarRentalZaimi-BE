using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;

internal class CreateCarCategoryCommandHandler(ICarCategoryService _carCategoryService) : ICommandHandler<CreateCarCategoryCommand, CarCategoryDto>
{
    public async Task<Result<CarCategoryDto>> Handle(CreateCarCategoryCommand request, CancellationToken cancellationToken)
        => await _carCategoryService.CreateAsync(request, cancellationToken);
}
