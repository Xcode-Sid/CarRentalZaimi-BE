using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class SubscribeDto : BaseDto<Guid>
{
    public string? Email { get; set; }
    public bool IsUnsubscribed { get; set; }
}
