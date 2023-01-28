using System.Linq;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.Users.Handlers
{
    [TestClass]
    public class ChangeProfileSettingsHandlerTests
    {
        ChangeProfileSettingsHandler _changeProfileSettingsHandler;
        int _userId;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);

            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();
            _userId = appUser.AppUserId;

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(appUser.AppUserId);

            _changeProfileSettingsHandler = new ChangeProfileSettingsHandler(
                bodyFitTrackerContext,
                userAccessorMock.Object
            );
        }

        [TestMethod]
        public void UserProfileShouldBeUpdatedBasedOffRequest()
        {
            ChangeProfileSettingsRequest request = new ChangeProfileSettingsRequest
            {
                Email = "test@gmail.com",
                Height = 70,
                UnitsOfMeasure = MeasurementSystem.Imperial
            };
            _changeProfileSettingsHandler.Handle(request);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
            AppUser appUser = bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == _userId).First();

            Assert.AreEqual("test@gmail.com", appUser.Email);
            Assert.AreEqual(70, appUser.Height);
        }
    }
}
