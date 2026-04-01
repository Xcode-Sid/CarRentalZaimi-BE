using FluentValidation;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Microsoft;

public class AuthenticateWithMicrosoftCommandValidator : AbstractValidator<AuthenticateWithMicrosoftCommand>
{
    public AuthenticateWithMicrosoftCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required");

        RuleFor(x => x.RedirectUri)
            .NotEmpty().WithMessage("RedirectUri is required");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required")
             .Must(role => role == "User").WithMessage("User type must be User.");
    }
}
