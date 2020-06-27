using System;
using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.BodyMeasurements.Handlers;
using Api.ApplicationLogic.BodyMeasurements.Requests;
using Api.ApplicationLogic.Errors;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using Api.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.BodyMeasurements.Handlers
{
    [TestClass]
    public class CreateOrEditBodyMeasurementHandlerTests
    {
        CreateOrEditBodyMeasurementHandler _createOrEditBodyMeasurementHandler;

        (int UserId, int IdOfMeasurement) _dom;
        (int UserId, int IdOfMeasurement) _bob;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser dom = new AppUser("dom@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            AppUser bob = new AppUser("bob@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            BodyMeasurement domsBodyMeasurement = new BodyMeasurement(dom, 11, 30, null, 130, DateTime.Today);
            BodyMeasurement bobsBodyMeasurement = new BodyMeasurement(bob, 11, 30, null, 130, DateTime.Today);
            bodyFitTrackerContext.AppUsers.Add(dom);
            bodyFitTrackerContext.AppUsers.Add(bob);
            bodyFitTrackerContext.BodyMeasurements.Add(domsBodyMeasurement);
            bodyFitTrackerContext.BodyMeasurements.Add(bobsBodyMeasurement);

            bodyFitTrackerContext.SaveChanges();

            _dom = (dom.AppUserId, domsBodyMeasurement.AppUserId);
            _bob = (bob.AppUserId, bobsBodyMeasurement.BodyMeasurementId);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(dom.AppUserId);

            _createOrEditBodyMeasurementHandler = new CreateOrEditBodyMeasurementHandler(bodyFitTrackerContext, userAccessorMock.Object);
        }

        [TestMethod]
        public void MeasurementShouldBeCreatedIfMeasurementIdToEditIsNull()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);

            Assert.AreEqual(1, bodyFitTrackerContext.BodyMeasurements.Where(x => x.AppUserId == _dom.UserId).Count());

            CreateOrEditBodyMeasurementRequest createMeasurementRequest = new CreateOrEditBodyMeasurementRequest
            {
                NeckCircumference = 10,
                WaistCircumference = 30,
                HipCircumference = null,
                Weight = 140,
                CreationDate = DateTime.Today,
            };

            _createOrEditBodyMeasurementHandler.Handle(createMeasurementRequest);

            bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false); // create new instance to ensure ef's local cache is not used

            Assert.AreEqual(2, bodyFitTrackerContext.BodyMeasurements.Where(x => x.AppUserId == _dom.UserId).Count());
        }

        [TestMethod]
        public void ARestExceptionShouldBeThrownIfMeasurementToEditIsNotfound()
        {
            CreateOrEditBodyMeasurementRequest createMeasurementRequest = new CreateOrEditBodyMeasurementRequest
            {
                IdOfBodyMeasurementToEdit = 1023
            };

            Assert.ThrowsException<RestException>(() => _createOrEditBodyMeasurementHandler.Handle(createMeasurementRequest));
        }

        [TestMethod]
        public void ARestExceptionShouldBeThrownIfCurrentUserTriesToEditAnotherUsersMeasurement()
        {
            CreateOrEditBodyMeasurementRequest editMeasurementRequest = new CreateOrEditBodyMeasurementRequest
            {
                IdOfBodyMeasurementToEdit = _bob.UserId
            };

            Assert.ThrowsException<RestException>(() => _createOrEditBodyMeasurementHandler.Handle(editMeasurementRequest));
        }

        [TestMethod]
        public void EditMeasurementShouldSucceedIfMeasurementBelongsToCurrentUser()
        {
            double newWeight = 150.6534;
            CreateOrEditBodyMeasurementRequest createMeasurementRequest = new CreateOrEditBodyMeasurementRequest
            {
                IdOfBodyMeasurementToEdit = _dom.IdOfMeasurement,
                NeckCircumference = 11,
                WaistCircumference = 12,
                HipCircumference = null,
                Weight = newWeight,
                CreationDate = DateTime.Today,
            };

            _createOrEditBodyMeasurementHandler.Handle(createMeasurementRequest);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);

            double actualNewWeight = bodyFitTrackerContext.BodyMeasurements.Where(x => x.AppUserId == _dom.UserId).First().Weight;
            
            Assert.AreEqual(newWeight, actualNewWeight, .01);
        }
    }
}