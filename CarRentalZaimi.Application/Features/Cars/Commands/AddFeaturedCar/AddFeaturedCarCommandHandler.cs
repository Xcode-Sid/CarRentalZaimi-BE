using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Cars.Commands.AddFeaturedCar;

public class AddFeaturedCarCommandHandler(ICarService _carService) : ICommandHandler<AddFeaturedCarCommand, bool>
{
    public async Task<Result<bool>> Handle(AddFeaturedCarCommand request, CancellationToken cancellationToken)
        => await _carService.AddFeaturedCarAsync(request, cancellationToken);
}
