using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllPagedAdditionalServices;

internal class GetAllPagedAdditionalServicesQueryHandler(IAdditionalServicesService _aditionalServiceService) : IQueryHandler<GetAllPagedAdditionalServicesQuery, PagedResponse<AdditionalServiceDto>>
{
    public async Task<Result<PagedResponse<AdditionalServiceDto>>> Handle(GetAllPagedAdditionalServicesQuery request, CancellationToken cancellationToken)
        => await _aditionalServiceService.GetAllPagedAsync(request, cancellationToken);
}
