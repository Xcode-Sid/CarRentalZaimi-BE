namespace CarRentalZaimi.Application.Common.Microsoft;

public class MicrosoftOAuthSettings
{
    public string? ClientId { get; set; }
    public string TenantId { get; set; } = "common";
    public string? TokenUrl { get; set; }
    public string? UserInfoUrl { get; set; }
}
