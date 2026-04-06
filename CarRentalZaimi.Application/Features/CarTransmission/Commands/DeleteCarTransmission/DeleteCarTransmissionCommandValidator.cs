using CarRentalZaimi.Application.Features.CarFuel.Commands.DeleteCarFuel;
using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarTransmission.Commands.DeleteCarTransmission;

internal class DeleteCarTransmissionCommandValidator : AbstractValidator<DeleteCarTransmissionCommand>
{
    public DeleteCarTransmissionCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");
    }
}
