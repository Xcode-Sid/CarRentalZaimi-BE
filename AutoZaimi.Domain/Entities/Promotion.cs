using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class Promotion : AuditedEntity<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public string? Code { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    //promotion could be for each car or each category
    public Car? Car { get; set; }
    public CarCategory? CarCategory { get; set; }
    public int? UsageLimit { get; set; }
    public int TimesUsed { get; set; }
}
