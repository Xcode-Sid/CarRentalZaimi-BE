using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Queries.GetAllCarCompanyModel;

internal class GetAllCarCompanyModelQueryHandler(ICarCompanyModelService _carCompanyModelService) : IQueryHandler<GetAllCarCompanyModelQuery, IEnumerable<CarCompanyModelDto>>
{
    public async Task<ApiResponse<IEnumerable<CarCompanyModelDto>>> Handle(GetAllCarCompanyModelQuery request, CancellationToken cancellationToken)
        => await _carCompanyModelService.GetAllAsync(request, cancellationToken);
}
