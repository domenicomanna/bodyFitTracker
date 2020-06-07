using Api.ApplicationLogic.Errors;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Api.Domain.Models;
using Api.Infrastructure.PasswordHashing;
using Api.Infrastructure.Security;
using Api.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.Users.Handlers
{
    [TestClass]
    public class LoginHandlerTests
    {
        LoginHandler _loginHandler;
        string _password = "dom";

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser dom = new AppUser("dom@gmail.com", _password, "", 10, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.AppUsers.Add(dom);
            bodyFitTrackerContext.SaveChanges();

            var passwordHasherMock = new Mock<IPasswordHasher>();
            var jwtGeneratorMock = new Mock<IJwtGenerator>();

            passwordHasherMock.Setup(x => x.ValidatePlainTextPassword(_password, _password, It.IsAny<string>())).Returns(true);
            jwtGeneratorMock.Setup(x => x.CreateToken(It.IsAny<AppUser>())).Returns("");

            _loginHandler = new LoginHandler(bodyFitTrackerContext, passwordHasherMock.Object, jwtGeneratorMock.Object);
        }

        [TestMethod]
        public void IfUserIsNotFoundARestExceptionShouldBeThrown()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = "d@gmail.com",
                Password = ""
            };

            Assert.ThrowsException<RestException>(() => _loginHandler.Handle(loginRequest));

        }

        [TestMethod]
        public void IfUserIsFoundButCredentialsAreNotValidARestExceptionShouldBeThrown()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = "dom@gmail.com",
                Password = "test"
            };

            Assert.ThrowsException<RestException>(() => _loginHandler.Handle(loginRequest));

        }

        [TestMethod]
        public void IfUserIsFoundButCredentialsAreValidLoginShoudSucceed()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = "dom@gmail.com",
                Password = _password
            };

            AppUserDTO appUserDTO = _loginHandler.Handle(loginRequest);
            Assert.IsNotNull(appUserDTO);

        }
    }
}