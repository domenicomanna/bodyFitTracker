using System;
using Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasData(
                    new AppUser(
                        "abc@gmail.com",
                        "abc",
                        "abc",
                        120,
                        60,
                        GenderType.Male,
                        MeasurementSystem.Imperial
                    )
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
                        DateAdded = DateTime.Today
                    },
                    new
                    {
                        BodyMeasurementId = 2,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserEmail = "abc@gmail.com",
                        DateAdded = DateTime.Today.AddDays(1)
                    },
                    new
                    {
                        BodyMeasurementId = 3,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserEmail = "abc@gmail.com",
                        DateAdded = DateTime.Today.AddDays(2)
                    }
                );
        }
    }
}