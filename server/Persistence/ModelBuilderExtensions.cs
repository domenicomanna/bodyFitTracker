using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasData(
                    new AppUser{
                        Email = "abc@gmail.com",
                        HashedPassword = "abc",
                        Salt = "abc",
                        Weight = 120,
                        Height = 60,
                        Gender = GenderType.Male,
                        MeasurementSystemPreference = MeasurementSystem.Imperial
                    }
                );
            
            modelBuilder.Entity<BodyMeasurement>()
                .HasData(
                    new BodyMeasurement{
                        BodyMeasurementId = 1,
                        NeckCircumference = 12,
                        WaistCircumference = 30,
                        Weight = 130,
                        AppUserEmail = "abc@gmail.com",
                    }
                );

            modelBuilder.Entity<BodyMeasurement>()
                .HasData(
                    new BodyMeasurement{
                        BodyMeasurementId = 2,
                        NeckCircumference = 12,
                        WaistCircumference = 28,
                        Weight = 125,
                        AppUserEmail = "abc@gmail.com",
                    }
                );

            modelBuilder.Entity<BodyMeasurement>()
                .HasData(
                    new BodyMeasurement{
                        BodyMeasurementId = 3,
                        NeckCircumference = 12,
                        WaistCircumference = 26,
                        Weight = 120,
                        AppUserEmail = "abc@gmail.com",
                    }
                );
        }
    }
}