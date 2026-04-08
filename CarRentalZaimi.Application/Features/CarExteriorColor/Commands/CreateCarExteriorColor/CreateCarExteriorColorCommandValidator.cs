using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarExteriorColor.Commands.CreateCarExteriorColor;

public class CreateCarExteriorColorCommandValidator : AbstractValidator<CreateCarExteriorColorCommand>
{
    public CreateCarExteriorColorCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
}
