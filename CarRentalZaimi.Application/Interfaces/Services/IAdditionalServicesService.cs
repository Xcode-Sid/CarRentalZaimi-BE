using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.CreateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;
using CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IAdditionalServicesService
{
    Task<Result<AdditionalServiceDto>> CreateAsync(CreateAdditionalServiceCommand request, CancellationToken cancellationToken = default);
    Task<Result<AdditionalServiceDto>> UpdateAsync(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteAdditionalServiceCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<AdditionalServiceDto>>> GetAllAsync(GetAllAdditionalServicesQuery request, CancellationToken cancellationToken = default);
}
