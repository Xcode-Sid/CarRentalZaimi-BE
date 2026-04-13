using CarRentalZaimi.Application.Features.BookingRequest.Commands.CancelBooking;
using FluentValidation;

namespace CarRentalZaimi.Application.Features.BookingRequest.Commands.CloseBooking;

internal class CloseBookingCommandValidator : AbstractValidator<CancelBookingCommand>
{
    public CloseBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
           .NotEmpty().WithMessage("Booking Id is required");
    }
}