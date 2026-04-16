using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Queries.GetAllPagedStatePrefixes;

internal class GetAllPagedStatePrefixesQueryHandler(IStatePrefixService _statePrefixService) : IQueryHandler<GetAllPagedStatePrefixesQuery, PagedResponse<StatePrefixDto>>
{
    public async Task<Result<PagedResponse<StatePrefixDto>>> Handle(GetAllPagedStatePrefixesQuery request, CancellationToken cancellationToken)
        => await _statePrefixService.GetAllPagedAsync(request, cancellationToken);
}