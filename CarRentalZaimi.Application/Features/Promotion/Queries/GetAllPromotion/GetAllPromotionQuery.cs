using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Promotion.Queries.GetAllPromotion;

public class GetAllPromotionQuery : IQuery<IEnumerable<PromotionDto>>;