using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.UpdatePromotion;

public record UpdatePromotionCommand : ICommand<PromotionDto>
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public decimal DiscountPercentage { get; set; }
    public int NumberOfDays { get; set; }
    public bool IsActive { get; set; }

}
