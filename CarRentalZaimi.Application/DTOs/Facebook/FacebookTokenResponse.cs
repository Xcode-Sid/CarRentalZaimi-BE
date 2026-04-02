namespace CarRentalZaimi.Application.DTOs.Facebook;

public class FacebookTokenResponse
{
    public string? AccessToken { get; set; }
    public string? TokenType { get; set; }
    public int? ExpiresIn { get; set; }
}
