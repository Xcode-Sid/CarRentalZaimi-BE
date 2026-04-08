using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarCompanyNameService
{
    Task<ApiResponse<CarCompanyNameDto>> CreateAsync(CreateCarCompanyNameCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarCompanyNameDto>> UpdateAsync(UpdateCarCompanyNameCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarCompanyNameCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarCompanyNameDto>>> GetAllAsync(GetAllCarCompanyNameQuery request, CancellationToken cancellationToken = default);
}
