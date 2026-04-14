using FluentValidation;

namespace CarRentalZaimi.Application.Features.Terms.Commands.CreateTerm;

internal class CreateTermCommandValidator : AbstractValidator<CreateTermCommand>
{
    public CreateTermCommandValidator()
    {
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