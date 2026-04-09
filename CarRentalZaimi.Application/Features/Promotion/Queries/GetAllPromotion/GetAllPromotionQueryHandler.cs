using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Promotion.Queries.GetAllPromotion;

internal class GetAllPromotionQueryHandler(IPromotionService _promotionService) : IQueryHandler<GetAllPromotionQuery, IEnumerable<PromotionDto>>
{
    public async Task<Result<IEnumerable<PromotionDto>>> Handle(GetAllPromotionQuery request, CancellationToken cancellationToken)
        => await _promotionService.GetAllAsync(request, cancellationToken);
}