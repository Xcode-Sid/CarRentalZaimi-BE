using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;
using CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarFuelService
{
    Task<Result<CarFuelDto>> CreateAsync(CreateCarFuelCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarFuelDto>> UpdateAsync(UpdateCarFuelCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarFuelCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarFuelDto>>> GetAllAsync(GetAllCarFuelsQuery request, CancellationToken cancellationToken = default);
}
