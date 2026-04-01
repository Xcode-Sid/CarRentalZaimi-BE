using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarReviewDto : BaseDto<Guid>
{
    public UserDto? User { get; set; }
    public CarDto? Post { get; set; }
    public float Rating { get; set; }
    public string? Comment { get; set; }
}
