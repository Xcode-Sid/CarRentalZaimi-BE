using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class UserDto : BaseDto<Guid>
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public RoleDto? Role { get; set; }
    public UserImageDto? Image { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Location { get; set; }
}
