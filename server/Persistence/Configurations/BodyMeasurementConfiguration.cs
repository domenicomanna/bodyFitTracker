using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BodyMeasurementConfiguration : IEntityTypeConfiguration<BodyMeasurement>
    {
        public void Configure(EntityTypeBuilder<BodyMeasurement> builder){
            builder
                .HasKey(b => b.BodyMeasurementId);
            builder
                .Property(b => b.BodyMeasurementId)
                .ValueGeneratedOnAdd();
        }
    }
}