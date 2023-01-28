using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.Users.DataTransferObjects;
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
    public class ChangePasswordHandlerTests
    {
        ChangePasswordHandler _changePasswordHandler;
        string _userPassword = "password";

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser appUser = new AppUser("abc@gmail.com", _userPassword, "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();

            var passwordHasherMock = new Mock<IPasswordHasher>();
            var userAccessorMock = new Mock<IUserAccessor>();

            userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(appUser.AppUserId);
            passwordHasherMock.Setup(x => x.ValidatePlainTextPassword(_userPassword, It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            passwordHasherMock.Setup(x => x.GeneratePassword(It.IsAny<string>())).Returns((string password) => (password, ""));

            _changePasswordHandler = new ChangePasswordHandler(bodyFitTrackerContext, passwordHasherMock.Object, userAccessorMock.Object);
        }

        [TestMethod]
        public void ChangingPasswordShouldFailIfCurrentPasswordIsIncorrect()
        {
            ChangePasswordRequest changePasswordRequest = new ChangePasswordRequest
            {
                CurrentPassword = "wrong password",
                NewPassword = "a",
                ConfirmedNewPassword = "a"
            };

            ChangePasswordResult changePasswordResult = _changePasswordHandler.Handle(changePasswordRequest);

            Assert.IsFalse(changePasswordResult.Succeeded);
        }

        [TestMethod]
        public void ChangingPasswordShouldSucceedIfCurrentPasswordIsCorrect()
        {
            ChangePasswordRequest changePasswordRequest = new ChangePasswordRequest
            {
                CurrentPassword = _userPassword,
                NewPassword = "abc",
                ConfirmedNewPassword = "abc"
            };

            ChangePasswordResult changePasswordResult = _changePasswordHandler.Handle(changePasswordRequest);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
            Assert.IsTrue(changePasswordResult.Succeeded);
            Assert.AreEqual("abc", bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == 1).First().HashedPassword);


        }
    }
}