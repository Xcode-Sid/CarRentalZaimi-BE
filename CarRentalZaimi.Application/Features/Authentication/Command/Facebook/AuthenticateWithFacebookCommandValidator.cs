using FluentValidation;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Facebook;

public class AuthenticateWithFacebookCommandValidator : AbstractValidator<AuthenticateWithFacebookCommand>
{
    public AuthenticateWithFacebookCommandValidator()
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
