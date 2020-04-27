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
                    new AppUser
                    {
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
                    new
                    {
                        BodyMeasurementId = 1,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserEmail = "abc@gmail.com",
                    },
                    new
                    {
                        BodyMeasurementId = 2,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserEmail = "abc@gmail.com",
                    },
                    new {
                        BodyMeasurementId = 3,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserEmail = "abc@gmail.com",
                    }
                );
        }
    }
}