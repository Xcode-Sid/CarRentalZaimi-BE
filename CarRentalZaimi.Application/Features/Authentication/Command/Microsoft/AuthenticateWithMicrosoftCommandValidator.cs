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

    }
}
