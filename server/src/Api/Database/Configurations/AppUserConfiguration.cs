using Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(a => a.AppUserId);
        builder.HasIndex(a => a.Email).IsUnique();
        builder.Property(a => a.AppUserId).ValueGeneratedOnAdd();
        builder.Property(a => a.Email).IsRequired();
        builder.Property(a => a.HashedPassword).IsRequired();
        builder.Property(a => a.Salt).IsRequired();
        builder.Property(a => a.Gender).IsRequired().HasConversion<string>();
        builder.Property(a => a.MeasurementSystemPreference).IsRequired().HasConversion<string>();
        builder.HasMany(b => b.BodyMeasurements).WithOne(a => a.AppUser).HasForeignKey(a => a.AppUserId).IsRequired();
    }
}
