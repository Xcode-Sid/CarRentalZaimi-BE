using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;

internal class CreateCarCompanyNameCommandHandler(ICarCompanyNameService _carCompanyNameService) : ICommandHandler<CreateCarCompanyNameCommand, CarCompanyNameDto>
{
    public async Task<ApiResponse<CarCompanyNameDto>> Handle(CreateCarCompanyNameCommand request, CancellationToken cancellationToken)
        => await _carCompanyNameService.CreateAsync(request, cancellationToken);
}
