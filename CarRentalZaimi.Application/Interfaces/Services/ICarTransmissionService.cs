using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarTransmissionService
{
    Task<Result<CarTransmissionDto>> CreateAsync(CreateCarTransmissionCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarTransmissionDto>> UpdateAsync(UpdateCarTransmissionCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarTransmissionCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarTransmissionDto>>> GetAllAsync(GetAllCarTransmissionQuery request, CancellationToken cancellationToken = default);
}
