using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Query;

namespace CarRentalZaimi.Application.Features.Promotion.Queries.GetPromotionByCarId;

public class GetPromotionByCarIdQuery : IQuery<decimal>
{
    public string? CarId { get; set; }
    public string? NumerOfDays { get; set; }
}
