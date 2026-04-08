using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.UpdateCarTransmission;

internal class UpdateCarTransmissionCommandValidator : AbstractValidator<UpdateCarTransmissionCommand>
{
    public UpdateCarTransmissionCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}
