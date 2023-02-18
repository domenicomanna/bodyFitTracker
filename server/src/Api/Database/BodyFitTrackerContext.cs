using Api.Domain.Models;
using Api.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class BodyFitTrackerContext : DbContext
{
    public BodyFitTrackerContext(DbContextOptions options)
        : base(options) { }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
    public DbSet<PasswordReset> PasswordResets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new BodyMeasurementConfiguration());
        modelBuilder.ApplyConfiguration(new PasswordResetConfiguration());
    }
}
