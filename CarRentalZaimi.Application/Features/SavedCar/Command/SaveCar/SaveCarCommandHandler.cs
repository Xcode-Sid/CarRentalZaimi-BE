using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.SavedCar.Command.SaveCar;

internal class SaveCarCommandHandler(ISavedCarService _savedCarService) : ICommandHandler<SaveCarCommand, bool>
{
    public async Task<Result<bool>> Handle(SaveCarCommand request, CancellationToken cancellationToken)
        => await _savedCarService.SaveCarAsync(request, cancellationToken);
}
