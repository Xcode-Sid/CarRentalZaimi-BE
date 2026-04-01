namespace CarRentalZaimi.Application.DTOs.Yahoo;

public class YahooTokenResponse
{
    public string? AccessToken { get; set; }
    public string? TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
    public string? IdToken { get; set; }
}
