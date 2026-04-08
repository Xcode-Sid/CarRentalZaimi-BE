using FluentValidation;

namespace CarRentalZaimi.Application.Features.SavedCar.Command.SaveCar;

internal class SaveCarCommandValidator : AbstractValidator<SaveCarCommand>
{
    public SaveCarCommandValidator()
    {
        RuleFor(x => x.UserId)
         .NotEmpty().WithMessage("User Id is required");

        RuleFor(x => x.CarId)
        .NotEmpty().WithMessage("Car Id is required");
    }
}

