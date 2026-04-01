using FluentValidation;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Google;

public class AuthenticateWithGoogleCommandValidator : AbstractValidator<AuthenticateWithGoogleCommand>
{
    public AuthenticateWithGoogleCommandValidator()
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