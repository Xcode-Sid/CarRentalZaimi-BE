using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Queries;

public class GetAllStatePrefixesQueryHandler(IStatePrefixService _statePrefixService) : IQueryHandler<GetAllStatePrefixesQuery, IEnumerable<StatePrefixDto>>
{
    public async Task<ApiResponse<IEnumerable<StatePrefixDto>>> Handle(GetAllStatePrefixesQuery request, CancellationToken cancellationToken)
        => await _statePrefixService.GetAllAsync(request, cancellationToken);
}
