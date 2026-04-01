using CarRentalZaimi.Application.DTOs.Base;
using CarRentalZaimi.Domain.Enums;

namespace CarRentalZaimi.Application.DTOs;

public class UserNotificationDto : BaseDto<Guid>
{
    public UserDto? User { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; }
    public UserNotificationType UserNotificationType { get; set; }
}
