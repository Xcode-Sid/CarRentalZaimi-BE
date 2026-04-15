using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.CreateOccupiedCarDays;

internal class CreateOccupiedCarDaysCommandHandler(IOccupiedCarDaysService _adsService) : ICommandHandler<CreateOccupiedCarDaysCommand, OccupiedCarDaysDto>
{
    public async Task<Result<OccupiedCarDaysDto>> Handle(CreateOccupiedCarDaysCommand request, CancellationToken cancellationToken)
        => await _adsService.CreateAsync(request, cancellationToken);
}

