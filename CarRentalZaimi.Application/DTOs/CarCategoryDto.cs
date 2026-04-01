using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarCategoryDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
