using FluentValidation;

namespace CarRentalZaimi.Application.Features.Privacy.Commands.UpdatePrivacy;

internal class UpdatePrivacyCommandValidator : AbstractValidator<UpdatePrivacyCommand>
{
    public UpdatePrivacyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id  is required");

        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required")
           .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");


        RuleFor(x => x.Description)
           .NotEmpty().WithMessage("Description  is required");

        RuleFor(x => x.Color)
           .NotEmpty().WithMessage("Color is required");


        RuleFor(x => x.Icon)
           .NotEmpty().WithMessage("Icon is required");

    }
}
