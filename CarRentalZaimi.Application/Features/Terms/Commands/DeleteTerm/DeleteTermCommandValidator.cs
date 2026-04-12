using FluentValidation;

namespace CarRentalZaimi.Application.Features.Terms.Commands.DeleteTerm;

internal class DeleteTermCommandValidator : AbstractValidator<DeleteTermCommand>
{
    public DeleteTermCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id  is required");

    }
}