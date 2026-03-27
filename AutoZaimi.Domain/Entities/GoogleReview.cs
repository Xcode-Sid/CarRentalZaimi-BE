using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;

public class GoogleReview : AuditedEntity<Guid>
{
    public string? GoogleReviewId { get; set; } 
    public string? AuthorName { get; set; } 
    public string? AuthorPhotoUrl { get; set; }
    public string? ProfileUrl { get; set; }
    public int Rating { get; set; }                             
    public string? Text { get; set; }
    public DateTime PublishedAt { get; set; }                  
    public bool IsVisible { get; set; } = true;
}
