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
                    new
                    {
                        AppUserId = 1,
                        Email = "mannadomenico2849@gmail.com",
                        HashedPassword = "Xt+eYgLCOWjNy3YBxMWvcDKOQoEVtVwIyCDp9qfo+ag=",
                        Salt = "HvJRurMKIz+KkIpQhw4DpA==",
                        Height = 60.0,
                        Gender = GenderType.Male,
                        MeasurementSystemPreference = MeasurementSystem.Imperial
                    },
                    new
                    {
                        AppUserId = 2,
                        Email = "bcdf@gmail.com",
                        HashedPassword = "Xt+eYgLCOWjNy3YBxMWvcDKOQoEVtVwIyCDp9qfo+ag=",
                        Salt = "HvJRurMKIz+KkIpQhw4DpA==",
                        Height = 55.0,
                        Gender = GenderType.Female,
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
                        Height = 60.0,
                        Weight = 125.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today,
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 2,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 120.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(-1),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 3,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 130.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 4,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 145.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 5,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 115.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 1,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 6,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 121.0,
                        BodyFatPercentage = 10.0,
                        HipCircumference = 20.0,
                        AppUserId = 2,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 7,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 122.0,
                        HipCircumference = 20.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 2,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 8,
                        NeckCircumference = 10.0,
                        WaistCircumference = 30.0,
                        Height = 60.0,
                        Weight = 125.0,
                        HipCircumference = 20.0,
                        BodyFatPercentage = 10.0,
                        AppUserId = 2,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 9,
                        NeckCircumference = 12.0,
                        WaistCircumference = 28.0,
                        Height = 60.0,
                        Weight = 126.6,
                        HipCircumference = 20.0,
                        BodyFatPercentage = 12.0,
                        AppUserId = 2,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    },
                    new
                    {
                        BodyMeasurementId = 10,
                        NeckCircumference = 11.0,
                        WaistCircumference = 29.0,
                        Height = 60.0,
                        Weight = 125.9,
                        HipCircumference = 20.0,
                        BodyFatPercentage = 11.0,
                        AppUserId = 2,
                        DateAdded = DateTime.Today.AddDays(-2),
                        Units = MeasurementSystem.Imperial
                    }
                );
        }
    }
}