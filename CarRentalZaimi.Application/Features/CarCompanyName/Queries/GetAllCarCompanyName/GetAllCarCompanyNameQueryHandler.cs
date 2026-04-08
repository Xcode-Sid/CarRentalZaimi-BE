using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;

internal class GetAllCarCompanyNameQueryHandler(ICarCompanyNameService _carCompanyNameService) : IQueryHandler<GetAllCarCompanyNameQuery, IEnumerable<CarCompanyNameDto>>
{
    public async Task<ApiResponse<IEnumerable<CarCompanyNameDto>>> Handle(GetAllCarCompanyNameQuery request, CancellationToken cancellationToken)
        => await _carCompanyNameService.GetAllAsync(request, cancellationToken);
}