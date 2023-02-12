using System;
using System.Collections.Generic;
using System.Linq;
using Api.Controllers.BodyMeasurements;
using Api.Controllers.BodyMeasurements.Common;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Api.Controllers.BodyMeasurements.Features;

namespace ApiTests.Controllers.BodyMeasurements.Features;

[TestClass]
public class GetAllBodyMeasurementsHandlerTests
{
    GetAllBodyMeasurementsHandler _getAllBodyMeasurementsHandler;

    [TestInitialize]
    public void SetUp()
    {
        BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
        AppUser appUser = new AppUser("abc@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
        bodyFitTrackerContext.AppUsers.Add(appUser);
        bodyFitTrackerContext.SaveChanges();

        bodyFitTrackerContext.BodyMeasurements.Add(
            new BodyMeasurement(appUser, 11, 12, null, 60, 120, DateTime.Today, MeasurementSystem.Imperial)
        );
        bodyFitTrackerContext.BodyMeasurements.Add(
            new BodyMeasurement(appUser, 11, 20, null, 60, 120, DateTime.Today, MeasurementSystem.Imperial)
        );
        bodyFitTrackerContext.SaveChanges();

        var userAccessorMock = new Mock<IUserAccessor>();
        userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(appUser.AppUserId);

        MapperConfiguration mapperConfiguration = new MapperConfiguration(opts =>
        {
            opts.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfiguration.CreateMapper();

        _getAllBodyMeasurementsHandler = new GetAllBodyMeasurementsHandler(
            bodyFitTrackerContext,
            mapper,
            userAccessorMock.Object
        );
    }

    [TestMethod]
    public void TwoBodyMeasurementsShouldBeReturned()
    {
        List<BodyMeasurementDTO> bodyMeasurements = _getAllBodyMeasurementsHandler.Handle();
        Assert.IsTrue(bodyMeasurements.Count() == 2);
    }
}
