using CarRentalZaimi.Application.DTOs.Base;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class BookingDto : BaseDto<Guid>
{
    public string? Reference { get; set; }
    public UserDto? User { get; set; }
    public CarDto? Car { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PaymentMethod { get; set; }
    public BookingStatus? Status { get; set; }
    public RefuzedByType? RefuzedBy { get; set; }
    public ICollection<BookingServiceDto>? BookingServices { get; set; }
}
