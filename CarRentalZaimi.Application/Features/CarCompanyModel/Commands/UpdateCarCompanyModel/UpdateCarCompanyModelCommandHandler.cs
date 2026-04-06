using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.UpdateCarCompanyModel;

internal class UpdateCarCompanyModelCommandHandler(ICarCompanyModelService _carCompanyModelService) : ICommandHandler<UpdateCarCompanyModelCommand, CarCompanyModelDto>
{
    public async Task<Result<CarCompanyModelDto>> Handle(UpdateCarCompanyModelCommand request, CancellationToken cancellationToken)
        => await _carCompanyModelService.UpdateAsync(request, cancellationToken);
}
