using System;
using Api.Domain.Models;
using Api.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Domain.ServiceTests
{
    [TestClass]
    public class BodyMeasurementConverterTests
    {
        [TestMethod]
        public void ImperialBodyMeasurementValuesConvertedToMetricTest()
        {
            AppUser appUser = new AppUser("", "", "", 60, GenderType.Female, MeasurementSystem.Imperial);
            BodyMeasurement bodyMeasurement = new BodyMeasurement(
                appUser,
                10,
                30,
                30,
                60,
                120,
                DateTime.Today,
                MeasurementSystem.Imperial
            );

            BodyMeasurement converted = BodyMeasurementConverter.Convert(
                bodyMeasurement,
                appUser.MeasurementSystemPreference,
                MeasurementSystem.Metric
            );

            double expectedBodyFatPercentage = bodyMeasurement.BodyFatPercentage;
            Assert.AreEqual(expectedBodyFatPercentage, converted.BodyFatPercentage, .01);
            Assert.AreEqual(152.4, converted.Height, .01);
        }

        [TestMethod]
        public void MetricBodyMeasurementValuesConvertedToImperialTest()
        {
            AppUser appUser = new AppUser("", "", "", 140, GenderType.Female, MeasurementSystem.Metric);
            BodyMeasurement bodyMeasurement = new BodyMeasurement(
                appUser,
                22,
                60,
                60,
                140,
                60,
                DateTime.Today,
                MeasurementSystem.Metric
            );

            BodyMeasurement converted = BodyMeasurementConverter.Convert(
                bodyMeasurement,
                appUser.MeasurementSystemPreference,
                MeasurementSystem.Imperial
            );

            double expectedBodyFatPercentage = bodyMeasurement.BodyFatPercentage;
            Assert.AreEqual(expectedBodyFatPercentage, converted.BodyFatPercentage, .01);
            Assert.AreEqual(55.118, converted.Height, .01);
        }

        [TestMethod]
        public void BodyMeasurementValuesShouldBeIdenticalIfSourceAndDestinationUnitsAreSame()
        {
            AppUser appUser = new AppUser("", "", "", 60, GenderType.Female, MeasurementSystem.Imperial);
            BodyMeasurement bodyMeasurement = new BodyMeasurement(
                appUser,
                10,
                30,
                30,
                60,
                120,
                DateTime.Today,
                MeasurementSystem.Imperial
            );

            BodyMeasurement converted = BodyMeasurementConverter.Convert(
                bodyMeasurement,
                appUser.MeasurementSystemPreference,
                appUser.MeasurementSystemPreference
            );

            Assert.AreEqual(bodyMeasurement.Height, converted.Height);
            Assert.AreEqual(bodyMeasurement.Weight, converted.Weight);
        }
    }
}
