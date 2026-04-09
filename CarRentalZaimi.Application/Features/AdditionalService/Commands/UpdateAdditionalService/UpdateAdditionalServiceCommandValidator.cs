using FluentValidation;

namespace CarRentalZaimi.Application.Features.AdditionalService.Commands.UpdateAdditionalService;

internal class UpdateAdditionalServiceCommandValidator : AbstractValidator<UpdateAdditionalServiceCommand>
{
    public UpdateAdditionalServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Icon)
            .NotEmpty().WithMessage("Icon is required");

        RuleFor(x => x.PricePerDay)
            .NotEmpty().WithMessage("Price per day is required");
    }
}