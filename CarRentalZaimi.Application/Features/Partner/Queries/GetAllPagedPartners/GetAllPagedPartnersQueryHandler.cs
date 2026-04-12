using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Partner.Queries.GetAllPagedPartners;

internal class GetAllPagedPartnersQueryHandler(IPartnerService _partnerService) : IQueryHandler<GetAllPagedPartnersQuery, PagedResponse<PartnerDto>>
{
    public async Task<Result<PagedResponse<PartnerDto>>> Handle(GetAllPagedPartnersQuery request, CancellationToken cancellationToken)
        => await _partnerService.GetAllPagedAsync(request, cancellationToken);
}
