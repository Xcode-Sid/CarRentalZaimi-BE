using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class EmailConfirmationTokenDto : BaseDto<Guid>
{
    public string? TokenHash { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
    public UserDto? User { get; set; }
}
