using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Privacy.Queries.GetAllPrivacies;

internal class GetAllPrivaciesQueryHandler(IPrivacyService _privacyService) : IQueryHandler<GetAllPrivaciesQuery, IEnumerable<PrivacyDto>>
{
    public async Task<Result<IEnumerable<PrivacyDto>>> Handle(GetAllPrivaciesQuery request, CancellationToken cancellationToken)
        => await _privacyService.GetAllAsync(request, cancellationToken);
}

