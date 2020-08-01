using System.Linq;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Api.Common;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.Users.Handlers
{
    [TestClass]
    public class ResetPasswordStepOneHandlerTests
    {
        ResetPasswordStepOneHandler _resetPasswordStepOneHandler;
        string _userEmail = "";

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);

            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();
            _userEmail = appUser.Email;

            var emailSender = new Mock<IEmailSender>();
            var passwordResetTokenGenerator = new Mock<IPasswordResetTokenGenerator>();

            emailSender.Setup(x => x.SendEmail(It.IsAny<EmailMessage>()));
            passwordResetTokenGenerator.Setup(x => x.CreateResetToken()).Returns(("reset-token"));

            _resetPasswordStepOneHandler = new ResetPasswordStepOneHandler(bodyFitTrackerContext, emailSender.Object, passwordResetTokenGenerator.Object);
        }

        [TestMethod]
        public void NothingShouldHappenIfEmailIsNotFound()
        {
            ResetPasswordStepOneRequest request = new ResetPasswordStepOneRequest
            {
                Email = "asdf"
            };

            _resetPasswordStepOneHandler.Handle(request);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
            Assert.AreEqual(0, bodyFitTrackerContext.PasswordResets.Count());
        }

        [TestMethod]
        public void IfEmailIsFoundAPasswordResetShouldBeCreated()
        {
            ResetPasswordStepOneRequest request = new ResetPasswordStepOneRequest
            {
                Email = _userEmail
            };

            _resetPasswordStepOneHandler.Handle(request);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
            Assert.AreEqual(1, bodyFitTrackerContext.PasswordResets.Count());
        }
    }
}