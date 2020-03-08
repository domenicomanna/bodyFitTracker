using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .HasKey(a => a.Email);
            builder
                .Property(a => a.HashedPassword)
                .IsRequired();
            builder
                .Property(a => a.Salt).
                IsRequired();
            builder
                .Property(a => a.Gender)
                .IsRequired()
                .HasConversion<string>();
            builder
                .HasMany(b => b.BodyMeasurements)
                .WithOne(a => a.AppUser)
                .HasForeignKey(a => a.AppUserEmail)
                .IsRequired();
        }
    }
}