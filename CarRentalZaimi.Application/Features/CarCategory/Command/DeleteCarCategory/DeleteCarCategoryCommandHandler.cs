using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.DeleteCarCategory;

internal class DeleteCarCategoryCommandHandler(ICarCategoryService _carCategoryService) : ICommandHandler<DeleteCarCategoryCommand, bool>
{
    public async Task<ApiResponse<bool>> Handle(DeleteCarCategoryCommand request, CancellationToken cancellationToken)
        => await _carCategoryService.DeleteAsync(request, cancellationToken);
}
