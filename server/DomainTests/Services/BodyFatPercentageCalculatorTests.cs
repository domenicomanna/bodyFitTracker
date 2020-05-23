using System;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Services
{
    [TestClass]
    public class BodyFatPercentageCalculatorTests
    {
        [TestInitialize]
        public void SetUp()
        {
        }

        [TestMethod]
        public void FatPercentageForMaleBasedOffMeasurementsShouldBeAbout24Percent()
        {
            AppUser appUser = new AppUser("", "", "", 0, 60, GenderType.Male, MeasurementSystem.Imperial);
            BodyMeasurement bodyMeasurement = new BodyMeasurement(appUser, 10, 30, null, DateTime.Today);
            double actual = BodyFatPercentageCalculator.CalculateBodyFatPercentage(appUser, bodyMeasurement);
            Assert.AreEqual(24.11, actual, .01);
        }

        [TestMethod]
        public void FatPercentageForFemaleBasedOffMeasurementsShouldBeAbout25Percent()
        {
            AppUser appUser = new AppUser("", "", "", 0, 60, GenderType.Female, MeasurementSystem.Imperial);
            BodyMeasurement bodyMeasurement = new BodyMeasurement(appUser, 10, 30, 30, DateTime.Today);
            double actual = BodyFatPercentageCalculator.CalculateBodyFatPercentage(appUser, bodyMeasurement);
            Assert.AreEqual(25.19, actual, .01);
        }
    }
}