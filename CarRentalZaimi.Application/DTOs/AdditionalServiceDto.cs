using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class AdditionalServiceDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsActive { get; set; }
    public ICollection<BookingServiceDto>? BookingServices { get; set; }
}
