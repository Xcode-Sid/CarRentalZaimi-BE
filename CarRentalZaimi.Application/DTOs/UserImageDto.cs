using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class UserImageDto : BaseDto<Guid>
{
    public UserDto? User { get; set; }
    public string? ImageName { get; set; }
    public string? ImagePath { get; set; }
}
