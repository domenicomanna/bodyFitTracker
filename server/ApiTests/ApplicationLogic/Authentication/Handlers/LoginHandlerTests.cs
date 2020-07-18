using Api.ApplicationLogic.Authentication.DataTransferObjects;
using Api.ApplicationLogic.Authentication.Handlers;
using Api.ApplicationLogic.Authentication.Requests;
using Api.ApplicationLogic.Errors;
using Api.ApplicationLogic.Interfaces;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.Domain.Models;
using Api.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.Authentication.Handlers
{
    [TestClass]
    public class LoginHandlerTests
    {
        LoginHandler _loginHandler;
        (string Email, string Password) _dom;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser dom = new AppUser("dom@gmail.com", "abc", "", 10, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.AppUsers.Add(dom);
            bodyFitTrackerContext.SaveChanges();

            _dom.Email = dom.Email;
            _dom.Password = dom.HashedPassword;

            var passwordHasherMock = new Mock<IPasswordHasher>();
            var jwtGeneratorMock = new Mock<IJwtGenerator>();

            passwordHasherMock.Setup(x => x.ValidatePlainTextPassword(dom.HashedPassword, dom.HashedPassword, It.IsAny<string>())).Returns(true);
            jwtGeneratorMock.Setup(x => x.CreateToken(It.IsAny<AppUser>())).Returns("");

            _loginHandler = new LoginHandler(bodyFitTrackerContext, passwordHasherMock.Object, jwtGeneratorMock.Object);
        }

        [TestMethod]
        public void IfUserIsNotFoundSignInShouldFail()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = "notFoundEmail@gmail.com",
                Password = ""
            };

            SignInResult signInResult = _loginHandler.Handle(loginRequest);
            Assert.IsFalse(signInResult.SignInWasSuccessful);

        }

        [TestMethod]
        public void IfUserIsFoundButCredentialsAreNotValidSignInShouldFail()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = _dom.Email,
                Password = "Invalid password"
            };

            SignInResult signInResult = _loginHandler.Handle(loginRequest);
            Assert.IsFalse(signInResult.SignInWasSuccessful);

        }

        [TestMethod]
        public void IfUserIsFoundButCredentialsAreValidLoginShoudSucceed()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = _dom.Email,
                Password = _dom.Password
            };

            SignInResult signInResult = _loginHandler.Handle(loginRequest);
            Assert.IsTrue(signInResult.SignInWasSuccessful);
        }
    }
}