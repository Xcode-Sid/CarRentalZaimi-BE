using FluentValidation;

namespace CarRentalZaimi.Application.Features.Authentication.Command.Yahoo;

public class AuthenticateWithYahooCommandValidator : AbstractValidator<AuthenticateWithYahooCommand>
{
    public AuthenticateWithYahooCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required");

        RuleFor(x => x.RedirectUri)
            .NotEmpty().WithMessage("RedirectUri is required");

    }

}
