using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class CarCompanyNameDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public ICollection<CarCompanyModelDto>? CarCompanyModels { get; set; }
}
