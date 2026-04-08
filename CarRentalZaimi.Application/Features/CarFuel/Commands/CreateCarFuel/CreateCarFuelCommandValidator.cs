using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.CreateCarFuel;

public class CreateCarFuelCommandValidator : AbstractValidator<CreateCarFuelCommand>
{
    public CreateCarFuelCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}
