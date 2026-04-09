using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Promotion.Queries.GetPromotionByCarId;

internal class GetPromotionByCarIdQueryHandler(IPromotionService _promotionService) : IQueryHandler<GetPromotionByCarIdQuery, decimal>
{
    public async Task<Result<decimal>> Handle(GetPromotionByCarIdQuery request, CancellationToken cancellationToken)
        => await _promotionService.GetPromotionByCarIdAsync(request, cancellationToken);
}
