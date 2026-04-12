using FluentValidation;

namespace CarRentalZaimi.Application.Features.Privacy.Commands.DeletePrivacy;

internal class DeletePrivacyCommandValidator : AbstractValidator<DeletePrivacyCommand>
{
    public DeletePrivacyCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id  is required");

    }
}