using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.UpdateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Queries.GetAllCarCompanyModel;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarCompanyModelService
{
    Task<Result<CarCompanyModelDto>> CreateAsync(CreateCarCompanyModelCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarCompanyModelDto>> UpdateAsync(UpdateCarCompanyModelCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarCompanyModelCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarCompanyModelDto>>> GetAllAsync(GetAllCarCompanyModelQuery request, CancellationToken cancellationToken = default);
}
