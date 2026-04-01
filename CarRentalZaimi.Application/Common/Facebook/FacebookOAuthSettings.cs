namespace CarRentalZaimi.Application.Common.Facebook;
public class FacebookOAuthSettings
{
    public string? AppId { get; set; }
    public string? AppSecret { get; set; }
    public string? TokenUrl { get; set; }
    public string? UserInfoUrl { get; set; }
    public string ApiVersion { get; set; } = "v18.0";
}
