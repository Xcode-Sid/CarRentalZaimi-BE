using CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;
using FluentValidation;

namespace CarRentalZaimi.Application.Features.Promotion.Commands.CreatePromotion;

internal class CreatePromotionCommandValidator : AbstractValidator<CreatePromotionCommand>
{
    public CreatePromotionCommandValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required")
           .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required")
            .MaximumLength(50).WithMessage("Code cannot exceed 50 characters");

        RuleFor(x => x.DiscountPercentage)
            .GreaterThan(0).WithMessage("Discount percentage must be greater than 0")
            .LessThan(100).WithMessage("Discount percentage must be greater than 100");

        RuleFor(x => x.NumberOfDays)
            .GreaterThan(0).WithMessage("Number of days must be greater than 0");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.CarId) || !string.IsNullOrEmpty(x.CarCategoryId))
            .WithMessage("Either CarId or CarCategoryId must be provided")
            .WithName("CarId");
    }
}