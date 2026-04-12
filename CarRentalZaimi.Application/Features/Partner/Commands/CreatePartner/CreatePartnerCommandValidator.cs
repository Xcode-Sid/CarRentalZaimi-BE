using FluentValidation;

namespace CarRentalZaimi.Application.Features.Partner.Commands.CreatePartner;

internal class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    public CreatePartnerCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Color)
           .NotEmpty().WithMessage("Color is required");


        RuleFor(x => x.Initials)
           .NotEmpty().WithMessage("Initials is required");

    }
}