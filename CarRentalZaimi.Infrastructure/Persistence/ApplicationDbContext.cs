using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Domain.Common;
using CarRentalZaimi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IUserContext userContext) : IdentityDbContext<User, Role, string>(options)
{
    public override int SaveChanges()
    {
        ApplyAuditInfo();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        var now = DateTime.UtcNow;
        var userId = userContext.UserId;
        var ip = userContext.IpAddress;

        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = now;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedIP = ip;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedOn = now;
                    entry.Entity.ModifiedBy = userId;
                    entry.Entity.ModifiedIP = ip;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedOn = now;
                    entry.Entity.DeletedBy = userId;
                    entry.Entity.DeletedIP = ip;
                    break;
            }
        }
    }


    //Entities
    public required DbSet<AdditionalService> AdditionalServices { get; set; }
    public required DbSet<Booking> Bookings { get; set; }
    public required DbSet<BookingService> BookingServices { get; set; }
    public required DbSet<Car> Cars { get; set; }
    public required DbSet<CarCategory> CarCategories { get; set; }
    public required DbSet<CarCompanyModel> CarCompanyModels { get; set; }
    public required DbSet<CarCompanyName> CarCompanyNames { get; set; }
    public required DbSet<CarExteriorColor> CarExteriorColors { get; set; }
    public required DbSet<CarFuel> CarFuels { get; set; }
    public required DbSet<CarImages> CarImages { get; set; }
    public required DbSet<CarInteriorColor> CarInteriorColors { get; set; }
    public required DbSet<CarReview> CarReviews { get; set; }
    public required DbSet<CarTransmission> CarTransmissions { get; set; }
    public required DbSet<CompanyProfile> CompanyProfiles { get; set; }
    public required DbSet<ContactMessage> ContactMessages { get; set; }
    public required DbSet<Language> Languages { get; set; }
    public required DbSet<PhoneConfirmationToken> PhoneConfirmationTokens { get; set; }
    public required DbSet<Promotion> Promotions { get; set; }
    public required DbSet<RefreshToken> RefreshTokens { get; set; }
    public required DbSet<SavedCar> SavedCars { get; set; }
    public required DbSet<UserImage> UserImages { get; set; }
    public required DbSet<UserNotification> UserNotifications { get; set; }
    public required DbSet<AppLog> AppLogs { get; set; }
    public required DbSet<UserDevice> UserDevices { get; set; }
    public required DbSet<StatePrefix> StatePrefixes { get; set; }
    public required DbSet<PasswordResetToken> PasswordResetTokens { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CompanyProfile>()
         .Property(e => e.WhyChooseUs)
         .HasColumnType("json");

        builder.Entity<CompanyProfile>()
         .Property(e => e.WorkingHours)
         .HasColumnType("json");

        builder.Entity<User>()
         .Property(e => e.Location)
         .HasColumnType("json");

        builder.Entity<UserDevice>(entity =>
        {
            entity.Property(d => d.Browser).HasConversion<string>();
            entity.Property(d => d.OperatingSystem).HasConversion<string>();
        });
    }
}
