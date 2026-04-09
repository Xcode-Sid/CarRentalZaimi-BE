using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.UpdatePromotion;

internal class UpdatePromotionCommandHandler(IPromotionService _promotionService) : ICommandHandler<UpdatePromotionCommand, PromotionDto>
{
    public async Task<Result<PromotionDto>> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        => await _promotionService.UpdateAsync(request, cancellationToken);
}