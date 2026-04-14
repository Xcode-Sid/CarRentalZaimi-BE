using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Terms.Queries.GetAllTerms;

internal class GetAllTermsQueryHandler(ITermsService _termsService) : IQueryHandler<GetAllTermsQuery, IEnumerable<TermsDto>>
{
    public async Task<Result<IEnumerable<TermsDto>>> Handle(GetAllTermsQuery request, CancellationToken cancellationToken)
        => await _termsService.GetAllAsync(request, cancellationToken);
}

