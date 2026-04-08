using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.UpdateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Queries.GetAllCarCompanyModel;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarCompanyModelService
{
    Task<ApiResponse<CarCompanyModelDto>> CreateAsync(CreateCarCompanyModelCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CarCompanyModelDto>> UpdateAsync(UpdateCarCompanyModelCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> DeleteAsync(DeleteCarCompanyModelCommand request, CancellationToken cancellationToken = default);
    Task<ApiResponse<IEnumerable<CarCompanyModelDto>>> GetAllAsync(GetAllCarCompanyModelQuery request, CancellationToken cancellationToken = default);
}
