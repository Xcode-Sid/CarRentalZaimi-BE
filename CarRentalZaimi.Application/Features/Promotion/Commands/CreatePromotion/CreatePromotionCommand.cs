using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;

public class CreatePromotionCommand : ICommand<PromotionDto>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int NumberOfDays { get; set; }
    public bool IsActive { get; set; }

    //just one of this for 1 promotion
    public string? CarId { get; set; }
    public string? CarCategoryId { get; set; }
}
