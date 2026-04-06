using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.DeleteCarExteriorColor;

internal class DeleteCarExteriorColorCommandValidator : AbstractValidator<DeleteCarExteriorColorCommand>
{
    public DeleteCarExteriorColorCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
