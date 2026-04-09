using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarReview.Commands.DeleteCarReview;

internal class DeleteCarReviewCommandValidator : AbstractValidator<DeleteCarReviewCommand>
{
    public DeleteCarReviewCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}