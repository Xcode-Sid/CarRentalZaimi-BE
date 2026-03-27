using CarRentalZaimi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options) { }


    //Entities
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<CarCategory> CarCategories => Set<CarCategory>();
    public DbSet<CarImages> CarImages => Set<CarImages>();
    public DbSet<CarReview> CarReviews => Set<CarReview>();
    public DbSet<CarFuel> CarFuels => Set<CarFuel>();
    public DbSet<CarCompanyModel> CarCompanyModels => Set<CarCompanyModel>();
    public DbSet<CarCompanyName> CarCompanyNames => Set<CarCompanyName>();
    public DbSet<CarExteriorColor> CarExteriorColors => Set<CarExteriorColor>();
    public DbSet<CarInteriorColor> CarInteriorColors => Set<CarInteriorColor>();
    public DbSet<CarTransmission> CarTransmissions => Set<CarTransmission>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<GoogleReview> GoogleReviews => Set<GoogleReview>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
