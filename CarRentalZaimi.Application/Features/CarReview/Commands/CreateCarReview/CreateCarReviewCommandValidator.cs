using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.CreateCarReview;

internal class CreateCarReviewCommandValidator : AbstractValidator<CreateCarReviewCommand>
{
    public CreateCarReviewCommandValidator()
    {
        RuleFor(x => x.UserId)
         .NotEmpty().WithMessage("User Id is required");

        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("Car Id is required");
    }
}