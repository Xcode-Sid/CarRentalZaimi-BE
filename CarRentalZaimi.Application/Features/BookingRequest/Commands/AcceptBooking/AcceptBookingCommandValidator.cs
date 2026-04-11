using FluentValidation;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.AcceptBooking;

internal class AcceptBookingCommandValidator : AbstractValidator<AcceptBookingCommand>
{
    public AcceptBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
           .NotEmpty().WithMessage("Booking Id is required");
    }
}