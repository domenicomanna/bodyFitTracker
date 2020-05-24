using Api.Domain.Models;
using Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence
{
     public class BodyFitTrackerContext : DbContext
    {
        public BodyFitTrackerContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BodyMeasurementConfiguration());
            modelBuilder.Seed();
        }
    }
}