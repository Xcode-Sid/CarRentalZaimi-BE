using FluentValidation;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.DeleteAdditionalService;

internal class DeleteAdditionalServiceCommandValidator : AbstractValidator<DeleteAdditionalServiceCommand>
{
    public DeleteAdditionalServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}