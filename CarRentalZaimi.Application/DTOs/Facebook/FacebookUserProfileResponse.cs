namespace CarRentalZaimi.Application.DTOs.Facebook;

public class FacebookUserProfileResponse
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public FacebookPicture? Picture { get; set; }
}
