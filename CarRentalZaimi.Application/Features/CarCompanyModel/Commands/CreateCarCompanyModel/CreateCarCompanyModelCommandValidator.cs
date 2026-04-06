using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;

internal class CreateCarCompanyModelCommandValidator : AbstractValidator<CreateCarCompanyModelCommand>
{
    public CreateCarCompanyModelCommandValidator()
    {
        RuleFor(x => x.CompanyNameId)
         .NotEmpty().WithMessage("Car company id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}