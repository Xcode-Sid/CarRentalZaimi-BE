using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarCompanyModelDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public CarCompanyNameDto? CarCompanyName { get; set; }
}
