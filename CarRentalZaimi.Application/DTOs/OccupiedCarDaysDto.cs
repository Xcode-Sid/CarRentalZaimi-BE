using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class OccupiedCarDaysDto : BaseDto<Guid>
{
    public CarDto? Car { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Type { get; set; }
}
