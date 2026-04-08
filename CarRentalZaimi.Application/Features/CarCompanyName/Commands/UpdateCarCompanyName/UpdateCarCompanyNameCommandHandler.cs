using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;

internal class UpdateCarCompanyNameCommandHandler(ICarCompanyNameService _carCompanyNameService) : ICommandHandler<UpdateCarCompanyNameCommand, CarCompanyNameDto>
{
    public async Task<ApiResponse<CarCompanyNameDto>> Handle(UpdateCarCompanyNameCommand request, CancellationToken cancellationToken)
        => await _carCompanyNameService.UpdateAsync(request, cancellationToken);
}

