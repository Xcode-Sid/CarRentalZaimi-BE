namespace CarRentalZaimi.Application.Common.Constants;

public static class JwtSettingKeys
{
    public const string SecretKey = "SecretKey";
    public const string Issuer = "Issuer";
    public const string Audience = "Audience";
    public const string ExpiryMinutes = "ExpiryMinutes";
    public const string RefreshTokenExpiryDays = "RefreshTokenExpiryDays";
}
