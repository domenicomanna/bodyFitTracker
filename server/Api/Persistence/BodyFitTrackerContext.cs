using Api.Domain.Models;
using Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence
{
    public class BodyFitTrackerContext : DbContext
    {
        public bool SeedData { get; set; } = true;

        public BodyFitTrackerContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
        public DbSet<PasswordReset> PasswordResets{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BodyMeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordResetConfiguration());

            if (SeedData) modelBuilder.Seed();
        }
    }
}