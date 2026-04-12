using CarRentalZaimi.Domain.Common;

namespace CarRentalZaimi.Domain.Entities;
public class CompanyProfile : AuditedEntity<Guid>
{
    public string? Name { get; set; }           
    public string? LogoUrl { get; set; }          
    public string? Tagline { get; set; }          
    public string? AboutText { get; set; }        
    public string? MissionTitle { get; set; }     
    public string? MissionDescription { get; set; }
    public string? WhyChooseUs { get; set; } //json in db

    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? WorkingHours { get; set; } //json in db

    // Social media
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? TwiterUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? WhatsAppNumber { get; set; }

    //stats
    public int Years { get; set; }
    public int Cars { get; set; }
    public int Cities { get; set; }
    public int Clients { get; set; }
}
