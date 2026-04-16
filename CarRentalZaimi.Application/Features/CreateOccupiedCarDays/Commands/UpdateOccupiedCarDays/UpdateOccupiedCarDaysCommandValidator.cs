using FluentValidation;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.UpdateOccupiedCarDays;

internal class UpdateOccupiedCarDaysCommandValidator : AbstractValidator<UpdateOccupiedCarDaysCommand>
{
    public UpdateOccupiedCarDaysCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Type)
           .NotEmpty().WithMessage("Type is required");
    }
}