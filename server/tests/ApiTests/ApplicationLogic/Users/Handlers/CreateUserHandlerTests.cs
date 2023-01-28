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
    public class CreateUserHandlerTests
    {
        CreateUserHandler _createUserHandler;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);

            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();

            var jwtGeneratorMock = new Mock<IJwtGenerator>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            jwtGeneratorMock.Setup(x => x.CreateToken(It.IsAny<AppUser>())).Returns("");
            passwordHasherMock.Setup(x => x.GeneratePassword(It.IsAny<string>())).Returns(("", ""));

            _createUserHandler = new CreateUserHandler(
                bodyFitTrackerContext,
                passwordHasherMock.Object,
                jwtGeneratorMock.Object
            );
        }

        [TestMethod]
        public void UserCreationShouldFailIfEmailAddressIsAlreadyTaken()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest { Email = "abc@gmail.com" };

            CreateUserResult createUserResult = _createUserHandler.Handle(createUserRequest);
            Assert.IsFalse(createUserResult.Succeeded);
        }

        [TestMethod]
        public void UserCreationShouldSucceedIfEmailAddressIsAvailable()
        {
            string emailAddress = "computer@gmail.com";
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                Email = emailAddress,
                Password = "abc",
                ConfirmedPassword = "abc",
                Gender = GenderType.Female,
                UnitsOfMeasure = MeasurementSystem.Imperial
            };

            CreateUserResult createUserResult = _createUserHandler.Handle(createUserRequest);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
            Assert.IsNotNull(bodyFitTrackerContext.AppUsers.Where(x => x.Email == emailAddress).First());
            Assert.IsTrue(createUserResult.Succeeded);
        }
    }
}
