using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Partner.Queries.GetAllPartners;

internal class GetAllPartnersQueryHandler(IPartnerService _partnerService) : IQueryHandler<GetAllPartnersQuery, IEnumerable<PartnerDto>>
{
    public async Task<Result<IEnumerable<PartnerDto>>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
        => await _partnerService.GetAllAsync(request, cancellationToken);
}
