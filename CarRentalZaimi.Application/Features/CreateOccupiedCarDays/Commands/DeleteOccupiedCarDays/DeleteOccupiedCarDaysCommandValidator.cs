using FluentValidation;

namespace CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.DeleteOccupiedCarDays;

internal class DeleteOccupiedCarDaysCommandValidator : AbstractValidator<DeleteOccupiedCarDaysCommand>
{
    public DeleteOccupiedCarDaysCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required");
    }
}
