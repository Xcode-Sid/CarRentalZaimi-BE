using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.UpdateCarFuel;

public class UpdateCarFuelCommandValidator : AbstractValidator<UpdateCarFuelCommand>
{
    public UpdateCarFuelCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}
