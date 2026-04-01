namespace CarRentalZaimi.Application.DTOs;

public class AuthenticationResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
    public UserDto? User { get; set; }
    public RoleDto? Role { get; set; }
}
