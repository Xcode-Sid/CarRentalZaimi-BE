using FluentValidation;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.DeletePromotion;

internal class DeletePromotionCommandValidator : AbstractValidator<DeletePromotionCommand>
{
    public DeletePromotionCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}