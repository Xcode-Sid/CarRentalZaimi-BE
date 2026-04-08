using CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalZaimi.Application.Features.Cars.Commands.AddFeaturedCar;

internal class AddFeaturedCarCommandValidator : AbstractValidator<AddFeaturedCarCommand>
{
    public AddFeaturedCarCommandValidator()
    {
        RuleFor(x => x.CarId)
        .NotEmpty().WithMessage("Id is required");
    }
}
