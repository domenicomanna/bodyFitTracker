using System;
using Api.Controllers.Users.Features;
using Api.Domain.Models;
using Api.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Controllers.Features.Users
{
    [TestClass]
    public class ValidateResetPasswordTokenHandlerTests
    {
        ValidateResetPasswordTokenHandler _validateResetPasswordTokenHandler;
        string _unexpiredToken = "unexpired";
        string _expiredToken = "expired";

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);

            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.PasswordResets.Add(
                new PasswordReset(_unexpiredToken, appUser.AppUserId, DateTime.Now.AddHours(10))
            );
            bodyFitTrackerContext.PasswordResets.Add(
                new PasswordReset(_expiredToken, appUser.AppUserId, DateTime.Now.AddHours(-1))
            );
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();

            _validateResetPasswordTokenHandler = new ValidateResetPasswordTokenHandler(bodyFitTrackerContext);
        }

        [TestMethod]
        public void ValidationShouldFailIfTokenIsNotFound()
        {
            ResetPasswordValidationResult result = _validateResetPasswordTokenHandler.Handle("random");
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void ValidationShouldFailIfTokenIsExipred()
        {
            ResetPasswordValidationResult result = _validateResetPasswordTokenHandler.Handle(_expiredToken);
            Assert.IsFalse(result.Succeeded);
        }

        [TestMethod]
        public void ValidationShouldPassIfTokenIsFoundAndNotExipred()
        {
            ResetPasswordValidationResult result = _validateResetPasswordTokenHandler.Handle(_unexpiredToken);
            Assert.IsTrue(result.Succeeded);
        }
    }
}
