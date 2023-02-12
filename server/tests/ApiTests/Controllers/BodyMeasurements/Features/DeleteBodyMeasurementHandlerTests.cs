using System;
using System.Linq;
using Api.Controllers.BodyMeasurements.Features;
using Api.Common.Errors;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.Controllers.BodyMeasurements.Features;

[TestClass]
public class DeleteBodyMeasurementHandlerTests
{
    DeleteBodyMeasurementHandler _deleteBodyMeasurementHandler;

    [TestInitialize]
    public void SetUp()
    {
        BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);

        AppUser dom = new AppUser("dom@gmail.com", "", "", 10, GenderType.Male, MeasurementSystem.Imperial); // will have an id of 1
        AppUser bob = new AppUser("bob@gmail.com", "", "", 10, GenderType.Male, MeasurementSystem.Imperial); // will have an id of 2
        bodyFitTrackerContext.BodyMeasurements.Add(
            new BodyMeasurement(dom, 11, 12, null, 60, 100, DateTime.Today, MeasurementSystem.Imperial)
        ); // will have id of 1
        bodyFitTrackerContext.BodyMeasurements.Add(
            new BodyMeasurement(bob, 11, 20, null, 60, 100, DateTime.Today, MeasurementSystem.Imperial)
        ); // will have an id of 2

        bodyFitTrackerContext.SaveChanges();

        var userAccessorMock = new Mock<IUserAccessor>();
        userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(dom.AppUserId);

        _deleteBodyMeasurementHandler = new DeleteBodyMeasurementHandler(
            bodyFitTrackerContext,
            userAccessorMock.Object
        );
    }

    [TestMethod]
    public void ARestExcpetionShouldBeThrownIfTheBodyMeasurementIsNotFound()
    {
        Assert.ThrowsException<RestException>(() => _deleteBodyMeasurementHandler.Handle(1230));
    }

    [TestMethod]
    public void ARestExcpetionShouldBeThrownIfTheUserTriesToDeleteAnotherUsersMeasurement()
    {
        Assert.ThrowsException<RestException>(() => _deleteBodyMeasurementHandler.Handle(2));
    }

    [TestMethod]
    public void TheUsersBodyMeasurementShouldBeDeletedIfFound()
    {
        BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
        Assert.IsNotNull(bodyFitTrackerContext.BodyMeasurements.Where(b => b.BodyMeasurementId == 1).First());

        _deleteBodyMeasurementHandler.Handle(1);

        bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);
        Assert.IsNull(bodyFitTrackerContext.BodyMeasurements.Where(b => b.BodyMeasurementId == 1).FirstOrDefault());
    }
}
