using FluentValidation;

namespace CarRentalZaimi.Application.Features.Subscribe.Commands.RemoveSubscription;

internal class RemoveSubscriptionCommandValidator : AbstractValidator<RemoveSubscriptionCommand>
{
    public RemoveSubscriptionCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");
    }
}
