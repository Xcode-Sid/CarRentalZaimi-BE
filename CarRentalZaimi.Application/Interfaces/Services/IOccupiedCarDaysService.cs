using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.CreateOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.DeleteOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.UpdateOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetCarCalendarData;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetOccupiedCarDays;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IOccupiedCarDaysService
{
    Task<Result<OccupiedCarDaysDto>> CreateAsync(CreateOccupiedCarDaysCommand request, CancellationToken cancellationToken = default);
    Task<Result<OccupiedCarDaysDto>> UpdateAsync(UpdateOccupiedCarDaysCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteOccupiedCarDaysCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<OccupiedCarDaysDto>>> GetCarCalendarDataAsync(GetCarCalendarDataQuery request, CancellationToken cancellationToken = default);
    Task<Result<PagedResponse<OccupiedCarDaysDto>>> GetGetOccupiedCarDaysAsync(GetOccupiedCarDaysQuery request, CancellationToken cancellationToken = default);
}

