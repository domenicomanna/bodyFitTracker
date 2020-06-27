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
        public void IfUserIsNotFoundARestExceptionShouldBeThrown()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = "notFoundEmail@gmail.com",
                Password = ""
            };

            Assert.ThrowsException<RestException>(() => _loginHandler.Handle(loginRequest));

        }

        [TestMethod]
        public void IfUserIsFoundButCredentialsAreNotValidARestExceptionShouldBeThrown()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = _dom.Email,
                Password = "Invalid password"
            };

            Assert.ThrowsException<RestException>(() => _loginHandler.Handle(loginRequest));

        }

        [TestMethod]
        public void IfUserIsFoundButCredentialsAreValidLoginShoudSucceed()
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Email = _dom.Email,
                Password = _dom.Password
            };

            AppUserDTO appUserDTO = _loginHandler.Handle(loginRequest);
            Assert.IsNotNull(appUserDTO);

        }
    }
}