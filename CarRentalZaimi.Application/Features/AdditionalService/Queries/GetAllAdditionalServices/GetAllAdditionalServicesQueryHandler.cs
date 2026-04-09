using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.AdditionalService.Queries.GetAllAdditionalServices
{
    internal class GetAllAdditionalServicesQueryHandler(IAdditionalServicesService _aditionalServicesService)  : IQueryHandler<GetAllAdditionalServicesQuery, IEnumerable<AdditionalServiceDto>>
    {
        public async Task<Result<IEnumerable<AdditionalServiceDto>>> Handle(GetAllAdditionalServicesQuery request, CancellationToken cancellationToken)
            => await _aditionalServicesService.GetAllAsync(request, cancellationToken);
    }
}
