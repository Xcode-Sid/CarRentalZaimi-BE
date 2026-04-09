using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;
using CarRentalZaimi.Application.Interfaces.Services;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;

internal class CreatePromotionCommandHandler(IPromotionService _promotionService) : ICommandHandler<CreatePromotionCommand, PromotionDto>
{
    public async Task<Result<PromotionDto>> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        => await _promotionService.CreateAsync(request, cancellationToken);
}