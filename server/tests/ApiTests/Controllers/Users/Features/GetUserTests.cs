using Api.Controllers.Users;
using Api.Domain.Models;
using Api.Database;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Api.Controllers.Users.Features;
using Api.Controllers.Users.Common;
using Api.Services;

namespace ApiTests.Controllers.Features.Users
{
    [TestClass]
    public class GetUserHandlerTests
    {
        GetUserHandler _getUserHandler;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);

            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.Add(appUser);
            bodyFitTrackerContext.SaveChanges();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new UsersMappingProfile());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(appUser.AppUserId);

            _getUserHandler = new GetUserHandler(bodyFitTrackerContext, mapper, userAccessorMock.Object);
        }

        [TestMethod]
        public void TheUserShouldBeReturned()
        {
            AppUserDTO appUserDTO = _getUserHandler.Handle();
            Assert.IsNotNull(appUserDTO);
        }
    }
}
