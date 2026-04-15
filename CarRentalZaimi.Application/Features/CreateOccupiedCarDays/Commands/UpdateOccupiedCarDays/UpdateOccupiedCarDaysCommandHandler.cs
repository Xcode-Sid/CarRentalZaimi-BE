using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.UpdateOccupiedCarDays;

internal class UpdateOccupiedCarDaysCommandHandler(IOccupiedCarDaysService _adsService) : ICommandHandler<UpdateOccupiedCarDaysCommand, OccupiedCarDaysDto>
{
    public async Task<Result<OccupiedCarDaysDto>> Handle(UpdateOccupiedCarDaysCommand request, CancellationToken cancellationToken)
        => await _adsService.UpdateAsync(request, cancellationToken);
}


