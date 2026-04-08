using FluentValidation;

namespace CarRentalZaimi.Application.Features.CarCategory.Command.UpdateCarCategory;

internal class UpdateCarCategoryCommandValidator : AbstractValidator<UpdateCarCategoryCommand>
{
    public UpdateCarCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

    }
}