using CarRentalZaimi.Application.DTOs.Base;

namespace CarRentalZaimi.Application.DTOs;

public class GoogleReviewDto : BaseDto<Guid>
{
    public string? GoogleReviewId { get; set; }
    public string? AuthorName { get; set; }
    public string? AuthorPhotoUrl { get; set; }
    public string? ProfileUrl { get; set; }
    public int Rating { get; set; }
    public string? Text { get; set; }
    public DateTime PublishedAt { get; set; }
    public bool IsVisible { get; set; }
}
