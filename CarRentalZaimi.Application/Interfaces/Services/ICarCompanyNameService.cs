using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface ICarCompanyNameService
{
    Task<Result<CarCompanyNameDto>> CreateAsync(CreateCarCompanyNameCommand request, CancellationToken cancellationToken = default);
    Task<Result<CarCompanyNameDto>> UpdateAsync(UpdateCarCompanyNameCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeleteCarCompanyNameCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CarCompanyNameDto>>> GetAllAsync(GetAllCarCompanyNameQuery request, CancellationToken cancellationToken = default);
}
