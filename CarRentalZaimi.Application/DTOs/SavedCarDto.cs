using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class SavedCarDto : BaseDto<Guid>
{
    public UserDto? User { get; set; }
    public CarDto? Car { get; set; }
}
