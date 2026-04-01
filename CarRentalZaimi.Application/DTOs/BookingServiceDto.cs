using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class BookingServiceDto : BaseDto<Guid>
{
    public BookingDto? Booking { get; set; }
    public AdditionalServiceDto? AdditionalService { get; set; }
    public decimal PricePerDay { get; set; }
}
