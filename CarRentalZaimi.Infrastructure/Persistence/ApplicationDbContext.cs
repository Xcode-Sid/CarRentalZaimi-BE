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
    public DbSet<Language> Languages { get; set; }
    public DbSet<PhoneConfirmationToken> PhoneConfirmationTokens { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<SavedCar> SavedCars { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<AppLog> AppLogs { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<StatePrefix> StatePrefixes { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Privacy> Privacies { get; set; }
    public DbSet<Terms> Terms { get; set; }
    public DbSet<Subscribe> Subscribes { get; set; }
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
