using FluentValidation;

namespace CarRentalZaimi.Application.Features.StatePrefixes.Commands.CreateStatePrefix;

public class CreateStatePrefixCommandValidator : AbstractValidator<CreateStatePrefixCommand>
{
    public CreateStatePrefixCommandValidator()
    {
        RuleFor(x => x.CountryName)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.PhonePrefix)
            .NotEmpty().WithMessage("Phone prefix is required");

        RuleFor(x => x.Flag)
            .NotEmpty().WithMessage("Flag is required");

        RuleFor(x => x.PhoneRegex)
            .NotEmpty().WithMessage("Phone regex is required");
    }
}
