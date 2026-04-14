using FluentValidation;

namespace CarRentalZaimi.Application.Features.CompanyProfile.Commands.AddCompanyProfileData;

internal class AddCompanyProfileDataCommandValidator : AbstractValidator<AddCompanyProfileDataCommand>
{
    public AddCompanyProfileDataCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Phone)
           .NotEmpty()
           .WithMessage("Phone number is required")
           .Matches(@"^\+?[1-9]\d{1,14}$")
           .WithMessage("Phone number must be a valid international format");

        RuleFor(x => x.Address)
           .NotEmpty().WithMessage("Address is required");
    }
}
