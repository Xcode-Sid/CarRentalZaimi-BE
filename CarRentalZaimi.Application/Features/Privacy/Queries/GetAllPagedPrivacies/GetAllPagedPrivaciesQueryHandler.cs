using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPagedPrivacies;

internal class GetAllPagedPrivaciesQueryHandler(IPrivacyService _privacyService) : IQueryHandler<GetAllPagedPrivaciesQuery, PagedResponse<PrivacyDto>>
{
    public async Task<Result<PagedResponse<PrivacyDto>>> Handle(GetAllPagedPrivaciesQuery request, CancellationToken cancellationToken)
        => await _privacyService.GetAllPagedAsync(request, cancellationToken);
}
