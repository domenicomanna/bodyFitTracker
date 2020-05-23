using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Configurations;

namespace Persistence
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