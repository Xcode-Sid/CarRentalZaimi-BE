namespace CarRentalZaimi.Domain.Common;

public class GeneralData : AuditedEntity<Guid>
{
    public required string Name { get; set; }
}

