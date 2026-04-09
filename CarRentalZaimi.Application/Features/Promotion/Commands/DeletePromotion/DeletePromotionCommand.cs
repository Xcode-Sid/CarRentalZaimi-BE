using CarRentalZaimi.Application.Interfaces.Command;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;

public class DeletePromotionCommand : ICommand<bool>
{
    public string? Id { get; set; }
}
