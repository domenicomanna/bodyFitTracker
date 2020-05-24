using System;
using Api.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Domain.ModelTests
{
    [TestClass]
    public class BodyMeasurementTests
    {

        [TestMethod]
        public void IfAppUserIsNullAnArgumentNullExceptionShouldBeThrown()
        {
            AppUser appUser = null;
            Assert.ThrowsException<ArgumentNullException>( () => new BodyMeasurement(appUser, 10, 30, null, DateTime.Today));
        }

        [TestMethod]
        public void IfAppUserIsFemaleAndHipCircumferenceIsNullAnArgumentNullExceptionShouldBeThrown()
        {
            AppUser appUser = new AppUser("", "", "", 60, 90, GenderType.Female, MeasurementSystem.Imperial);
            Assert.ThrowsException<ArgumentNullException>( () => new BodyMeasurement(appUser, 10, 30, null, DateTime.Today));
        }
    }
}