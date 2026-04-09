using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;

internal class DeletePromotionCommandHandler(IPromotionService _promotionService) : ICommandHandler<DeletePromotionCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        => await _promotionService.DeleteAsync(request, cancellationToken);
}