using CarRentalZaimi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options) { }


    //Entities
    public DbSet<AdditionalService> AdditionalServices { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingService> BookingServices { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarCategory> CarCategories { get; set; }
    public DbSet<CarCompanyModel> CarCompanyModels { get; set; }
    public DbSet<CarCompanyName> CarCompanyNames { get; set; }
    public DbSet<CarExteriorColor> CarExteriorColors { get; set; }
    public DbSet<CarFuel> CarFuels { get; set; }
    public DbSet<CarImages> CarImages { get; set; }
    public DbSet<CarInteriorColor> CarInteriorColors { get; set; }
    public DbSet<CarReview> CarReviews { get; set; }
    public DbSet<CarTransmission> CarTransmissions { get; set; }
    public DbSet<CompanyProfile> CompanyProfiles { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<EmailConfirmationToken> EmailConfirmationTokens { get; set; }
    public DbSet<GoogleReview> GoogleReviews { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<PhoneConfirmationToken> PhoneConfirmationTokens { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<SavedCar> SavedCars { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CompanyProfile>()
         .Property(e => e.WhyChooseUs)
         .HasColumnType("json");

        builder.Entity<CompanyProfile>()
         .Property(e => e.WorkingHours)
         .HasColumnType("json");
    }
}
