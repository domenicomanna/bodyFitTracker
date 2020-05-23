using System;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Models
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