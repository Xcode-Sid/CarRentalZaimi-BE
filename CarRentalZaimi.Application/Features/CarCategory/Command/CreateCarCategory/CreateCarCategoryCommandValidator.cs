using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.CreateCarCategory;

internal class CreateCarCategoryCommandValidator : AbstractValidator<CreateCarCategoryCommand>
{
    public CreateCarCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
}