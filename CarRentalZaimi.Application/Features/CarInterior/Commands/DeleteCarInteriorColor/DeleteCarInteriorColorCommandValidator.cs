using CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInterior;
using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.DeleteCarInteriorColor;

internal class DeleteCarInteriorColorCommandValidator : AbstractValidator<DeleteCarInteriorColorCommand>
{
    public DeleteCarInteriorColorCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
