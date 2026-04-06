using FluentValidation;

namespace CarRentalZaimi.Application.Features.Phone.Commands.ConfirmPhone;

public class ConfirmPhoneCommandValidator : AbstractValidator<ConfirmPhoneCommand>
{
    public ConfirmPhoneCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required")
            .MaximumLength(100)
            .WithMessage("User ID cannot exceed 100 characters");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Token is required")
            .MinimumLength(1)
            .WithMessage("Token cannot be empty");
    }
}
