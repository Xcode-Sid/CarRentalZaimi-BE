using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;

internal class DeleteCarCompanyModelCommandHandler(ICarCompanyModelService _carCompanyModelService) : ICommandHandler<DeleteCarCompanyModelCommand, bool>
{
    public async Task<ApiResponse<bool>> Handle(DeleteCarCompanyModelCommand request, CancellationToken cancellationToken)
        => await _carCompanyModelService.DeleteAsync(request, cancellationToken);
}
