using CarRentalZaimi.Application.DTOs.Base;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class BookingDto : BaseDto<Guid>
{
    public UserDto? User { get; set; }
    public CarDto? Car { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus? Status { get; set; }
}
