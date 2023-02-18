using System;
using Api.Controllers.BodyMeasurements;
using Api.Controllers.BodyMeasurements.Common;
using Api.Common.Exceptions;
using Api.Domain.Models;
using Api.Database;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Api.Controllers.BodyMeasurements.Features;
using Api.Services;

namespace ApiTests.Controllers.BodyMeasurements.Features;

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
        bodyFitTrackerContext.BodyMeasurements.Add(
            new BodyMeasurement(appUser, 11, 12, null, 60, 120, DateTime.Today, MeasurementSystem.Imperial)
        );
        bodyFitTrackerContext.SaveChanges();

        var userAccessorMock = new Mock<IUserAccessor>();
        userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(appUser.AppUserId);

        MapperConfiguration mapperConfiguration = new MapperConfiguration(opts =>
        {
            opts.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfiguration.CreateMapper();

        _getBodyMeasurementHandler = new GetBodyMeasurementHandler(
            bodyFitTrackerContext,
            mapper,
            userAccessorMock.Object
        );
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
