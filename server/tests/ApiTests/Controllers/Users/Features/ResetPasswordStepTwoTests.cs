using System;
using System.Linq;
using Api.Controllers.Users.Features;
using Api.Domain.Models;
using Api.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Api.Services;

namespace ApiTests.Controllers.Features.Users
{
    [TestClass]
    public class ResetPasswordStepTwoHandlerTests
    {
        ResetPasswordStepTwoHandler _resetPasswordStepTwoHandler;
        string _appUserEmail;
        string _passwordResetToken = "abcdefg";

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            _appUserEmail = appUser.Email;
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();

            // add the password reset record after the app user has been added, so the appUserId is generated
            bodyFitTrackerContext.PasswordResets.Add(
                new PasswordReset(_passwordResetToken, appUser.AppUserId, DateTime.Now.AddHours(10))
            );
            bodyFitTrackerContext.SaveChanges();

            var passwordHasherMock = new Mock<IPasswordHasher>();

            passwordHasherMock
                .Setup(x => x.GeneratePassword(It.IsAny<string>()))
                .Returns((string password) => (password, ""));

            _resetPasswordStepTwoHandler = new ResetPasswordStepTwoHandler(
                bodyFitTrackerContext,
                passwordHasherMock.Object
            );
        }

        [TestMethod]
        public void UsersPasswordShouldBeSuccessfullyUpdatedAssumingTokenIsFoundAndNotExipred()
        // see token validation tests to see what happens if token is invalid
        {
            ResetPasswordStepTwoRequest request = new ResetPasswordStepTwoRequest
            {
                NewPassword = "hello",
                ConfirmedNewPassword = "hello",
                ResetPasswordToken = _passwordResetToken
            };

            ResetPasswordStepTwoResult result = _resetPasswordStepTwoHandler.Handle(request);
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
            AppUser appUser = bodyFitTrackerContext.AppUsers.Where(x => x.Email == _appUserEmail).First();

            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(request.NewPassword, appUser.HashedPassword);
        }
    }
}
