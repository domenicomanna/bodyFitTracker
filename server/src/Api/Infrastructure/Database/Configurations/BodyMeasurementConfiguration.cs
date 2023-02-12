using Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Database.Configurations;

public class BodyMeasurementConfiguration : IEntityTypeConfiguration<BodyMeasurement>
{
    public void Configure(EntityTypeBuilder<BodyMeasurement> builder)
    {
        builder.HasKey(b => b.BodyMeasurementId);
        builder.Property(b => b.BodyMeasurementId).ValueGeneratedOnAdd();
        builder.Property(b => b.Units).IsRequired().HasConversion<string>();
    }
}
