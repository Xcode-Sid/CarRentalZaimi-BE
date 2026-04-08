using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarTransmission;

internal class GetAllCarTransmissionQueryHandler(ICarTransmissionService _carTransmissionService) : IQueryHandler<GetAllCarTransmissionQuery, IEnumerable<CarTransmissionDto>>
{
    public async Task<ApiResponse<IEnumerable<CarTransmissionDto>>> Handle(GetAllCarTransmissionQuery request, CancellationToken cancellationToken)
        => await _carTransmissionService.GetAllAsync(request, cancellationToken);
}
