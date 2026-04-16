using FluentValidation;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.CreateOccupiedCarDays;

internal class CreateOccupiedCarDaysCommandValidator : AbstractValidator<CreateOccupiedCarDaysCommand>
{
    public CreateOccupiedCarDaysCommandValidator()
    {
        RuleFor(x => x.CarId)
           .NotEmpty().WithMessage("Car Id is required");

        RuleFor(x => x.Type)
           .NotEmpty().WithMessage("Type is required");
    }
}