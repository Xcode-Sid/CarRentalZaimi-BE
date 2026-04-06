using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;

internal class CreateCarCompanyModelCommandHandler(ICarCompanyModelService _carCompanyModelService) : ICommandHandler<CreateCarCompanyModelCommand, CarCompanyModelDto>
{
    public async Task<Result<CarCompanyModelDto>> Handle(CreateCarCompanyModelCommand request, CancellationToken cancellationToken)
        => await _carCompanyModelService.CreateAsync(request, cancellationToken);
}
