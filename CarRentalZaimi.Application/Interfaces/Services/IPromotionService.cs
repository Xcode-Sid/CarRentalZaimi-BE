using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;
using CarRentalZaimi.Application.Features.Promotion.Commands.UpdatePromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetAllPromotion;
using CarRentalZaimi.Application.Features.Promotion.Queries.GetPromotionByCarId;

namespace CarRentalZaimi.Application.Interfaces.Services;

public interface IPromotionService
{
    Task<Result<PromotionDto>> CreateAsync(CreatePromotionCommand request, CancellationToken cancellationToken = default);
    Task<Result<PromotionDto>> UpdateAsync(UpdatePromotionCommand request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(DeletePromotionCommand request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PromotionDto>>> GetAllAsync(GetAllPromotionQuery request, CancellationToken cancellationToken = default);
    Task<Result<decimal>> GetPromotionByCarIdAsync(GetPromotionByCarIdQuery request, CancellationToken cancellationToken = default);
}
