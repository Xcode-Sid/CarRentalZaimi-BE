using CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInterior;
using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarInterior.Commands.CreateCarInteriorColor;

internal class CreateCarInteriorColorCommandValidator : AbstractValidator<CreateCarInteriorColorCommand>
{
    public CreateCarInteriorColorCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
}