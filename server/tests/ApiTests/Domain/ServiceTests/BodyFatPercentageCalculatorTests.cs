using System;
using Api.Domain.Models;
using Api.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Domain.ServiceTests;

[TestClass]
public class BodyFatPercentageCalculatorTests
{
    [TestInitialize]
    public void SetUp() { }

    [TestMethod]
    public void FatPercentageForMaleBasedOffMeasurementsShouldBeAbout24Percent()
    {
        AppUser appUser = new AppUser("", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
        BodyMeasurement bodyMeasurement = new BodyMeasurement(
            appUser,
            10,
            30,
            null,
            60,
            60,
            DateTime.Today,
            MeasurementSystem.Imperial
        );
        double actual = BodyFatPercentageCalculator.CalculateBodyFatPercentage(bodyMeasurement);
        Assert.AreEqual(24.11, actual, .01);
    }

    [TestMethod]
    public void FatPercentageForFemaleBasedOffMeasurementsShouldBeAbout25Percent()
    {
        AppUser appUser = new AppUser("", "", "", 60, GenderType.Female, MeasurementSystem.Imperial);
        BodyMeasurement bodyMeasurement = new BodyMeasurement(
            appUser,
            10,
            30,
            30,
            60,
            60,
            DateTime.Today,
            MeasurementSystem.Imperial
        );
        double actual = BodyFatPercentageCalculator.CalculateBodyFatPercentage(bodyMeasurement);
        Assert.AreEqual(25.19, actual, .01);
    }
}
