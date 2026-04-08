using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;

public class CreateCarCompanyNameCommandValidator : AbstractValidator<CreateCarCompanyNameCommand>
{
    public CreateCarCompanyNameCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
}
