using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.CreateCarTransmission;

internal class CreateCarTransmissionCommandValidator : AbstractValidator<CreateCarTransmissionCommand>
{
    public CreateCarTransmissionCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
}
