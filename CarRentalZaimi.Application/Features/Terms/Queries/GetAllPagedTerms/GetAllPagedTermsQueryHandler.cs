using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Terms.Queries.GetAllPagedTerms;

internal class GetAllPagedTermsQueryHandler(ITermsService _termsService) : IQueryHandler<GetAllPagedTermsQuery, PagedResponse<TermsDto>>
{
    public async Task<Result<PagedResponse<TermsDto>>> Handle(GetAllPagedTermsQuery request, CancellationToken cancellationToken)
        => await _termsService.GetAllPagedAsync(request, cancellationToken);
}

