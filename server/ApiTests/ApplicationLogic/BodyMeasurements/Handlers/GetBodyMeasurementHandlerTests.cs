using System;
using Api.ApplicationLogic.BodyMeasurements;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.ApplicationLogic.BodyMeasurements.Handlers;
using Api.ApplicationLogic.Errors;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using Api.Persistence;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.BodyMeasurements.Handlers
{
    [TestClass]
    public class GetBodyMeasurementHandlerTests
    {
        GetBodyMeasurementHandler _getBodyMeasurementHandler;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            bodyFitTrackerContext.AppUsers.Add(appUser);
            bodyFitTrackerContext.BodyMeasurements.Add(new BodyMeasurement(appUser, 11, 12, null, 60, 120, DateTime.Today));
            bodyFitTrackerContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(appUser.AppUserId);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new BodyMeasurementsMappingProfile());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();


            _getBodyMeasurementHandler = new GetBodyMeasurementHandler(bodyFitTrackerContext, mapper, userAccessorMock.Object);
        }


        [TestMethod]
        public void ARestExceptionShouldBeThrownIfATheBodyMeasurementIsNotFound()
        {
            Assert.ThrowsException<RestException>(() => _getBodyMeasurementHandler.Handle(1123));
        }


        [TestMethod]
        public void TheBodyMeasurementShouldBeReturnedIfFound()
        {
            BodyMeasurementDTO bodyMeasurementDTO = _getBodyMeasurementHandler.Handle(1);
            Assert.IsNotNull(bodyMeasurementDTO);
        }
    }
}