using FluentValidation;

namespace CarRentalZaimi.Application.Features.Authentication;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Firstname)
           .NotEmpty().WithMessage("Firstname name is required")
           .MaximumLength(50).WithMessage("Firstname cannot exceed 50 characters");

        RuleFor(x => x.Lastname)
           .NotEmpty().WithMessage("Lastname name is required")
           .MaximumLength(50).WithMessage("Lastname cannot exceed 50 characters");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username name is required")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Phone)
           .NotEmpty()
           .WithMessage("Phone number is required")
           .Matches(@"^\+?[1-9]\d{1,14}$")
           .WithMessage("Phone number must be a valid international format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).{8,}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role name is required")
            .Must(role => role == "User").WithMessage("User type must be User."); //TODO check later not static

    }
}
