using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.UpdateCarReview;

internal class UpdateCarReviewCommandValidator : AbstractValidator<UpdateCarReviewCommand>
{
    public UpdateCarReviewCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}