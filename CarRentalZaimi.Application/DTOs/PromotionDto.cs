using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class PromotionDto : BaseDto<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public CarDto? Car { get; set; }
    public CarCategoryDto? CarCategory { get; set; }
    public int? UsageLimit { get; set; }
    public int TimesUsed { get; set; }
}
