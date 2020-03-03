using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      
            modelBuilder.Entity<AppUser>()
                .HasKey(a => a.Email);
            modelBuilder.Entity<AppUser>()
                .Property(a => a.HashedPassword)
                .IsRequired();
            modelBuilder.Entity<AppUser>()
                .Property(a => a.Salt).
                IsRequired();
            modelBuilder.Entity<AppUser>()
                .Property(a => a.Gender)
                .IsRequired()
                .HasConversion<string>();
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.BodyMeasurements)
                .WithOne(a => a.AppUser)
                .IsRequired();

            modelBuilder.Entity<BodyMeasurement>()
                .HasKey(b => b.BodyMeasurementId);
            modelBuilder.Entity<BodyMeasurement>()
                .Property(b => b.BodyMeasurementId)
                .ValueGeneratedOnAdd();
        }
    }
}