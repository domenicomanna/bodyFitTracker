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
                    new {
                        AppUserId = 1,
                        Email = "abc@gmail.com",
                        HashedPassword = "abc",
                        Salt = "abc",
                        Weight = 120.0,
                        Height = 60.0,
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
                        AppUserId = 1,
                        DateAdded = DateTime.Today
                    },
                    new
                    {
                        BodyMeasurementId = 2,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(1)
                    },
                    new
                    {
                        BodyMeasurementId = 3,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 4,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 5,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 6,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 7,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 8,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 9,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    },
                    new
                    {
                        BodyMeasurementId = 10,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(2)
                    }
                );
        }
    }
}