using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.DeleteOccupiedCarDays;

internal class DeleteOccupiedCarDaysCommandHandler(IOccupiedCarDaysService _adsService) : ICommandHandler<DeleteOccupiedCarDaysCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteOccupiedCarDaysCommand request, CancellationToken cancellationToken)
        => await _adsService.DeleteAsync(request, cancellationToken);
}


