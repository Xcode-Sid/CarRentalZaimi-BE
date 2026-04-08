using CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInterior;
using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.UpdateCarInteriorColor;

internal class UpdateCarInteriorColorCommandValidator : AbstractValidator<UpdateCarInteriorColorCommand>
{
    public UpdateCarInteriorColorCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}