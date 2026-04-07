using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;

internal class DeleteCarFuelCommandValidator : AbstractValidator<DeleteCarFuelCommand>
{
    public DeleteCarFuelCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
