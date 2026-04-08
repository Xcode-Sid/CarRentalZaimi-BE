using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarTransmissionService
{
    Task<ApiResponse<CarTransmissionDto>> CreateAsync(CreateCarTransmissionCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarTransmissionDto>> UpdateAsync(UpdateCarTransmissionCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarTransmissionCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarTransmissionDto>>> GetAllAsync(GetAllCarTransmissionQuery request, CancellationToken cancellationToken = default);
}
