using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Services
{
    [TestClass]
    public class BodyFatPercentageCalculatorTests
    {
        BodyMeasurement _bodyMeasurement;

        [TestInitialize]
        public void SetUp()
        {
            _bodyMeasurement = new BodyMeasurement { WaistCircumference = 30, NeckCircumference = 10 };
        }

        [TestMethod]
        public void FatPercentageForMaleBasedOffMeasurementsShouldBeAbout24Percent()
        {
            AppUser appUser = new AppUser { Height = 60, Gender = GenderType.Male };
            double actual = BodyFatPercentageCalculator.CalculateBodyFatPercentage(appUser, _bodyMeasurement);
            Assert.AreEqual(24.11, actual, .01);
        }

        [TestMethod]
        public void FatPercentageForFemaleBasedOffMeasurementsShouldBeAbout25Percent()
        {
            AppUser appUser = new AppUser { Height = 60, Gender = GenderType.Female };
            _bodyMeasurement.HipCircumference = 30;
            double actual = BodyFatPercentageCalculator.CalculateBodyFatPercentage(appUser, _bodyMeasurement);
            Assert.AreEqual(25.19, actual, .01);
        }
    }
}