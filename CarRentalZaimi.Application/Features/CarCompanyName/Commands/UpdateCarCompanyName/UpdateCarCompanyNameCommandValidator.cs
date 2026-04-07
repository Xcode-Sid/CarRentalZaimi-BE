using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;

internal class UpdateCarCompanyNameCommandValidator : AbstractValidator<UpdateCarCompanyNameCommand>
{
    public UpdateCarCompanyNameCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}