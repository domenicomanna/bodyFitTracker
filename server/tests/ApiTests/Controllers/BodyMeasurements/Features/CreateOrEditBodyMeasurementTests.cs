using System;
using System.Collections.Generic;
using System.Linq;
using Api.Common.Errors;
using Api.Common.Interfaces;
using Api.Controllers.BodyMeasurements.Features;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentValidation.Results;
using FluentValidation.TestHelper;

namespace ApiTests.Controllers.BodyMeasurements.Features
{
    [TestClass]
    public class CreateOrEditBodyMeasurementTests
    {
        CreateOrEditBodyMeasurementHandler _createOrEditBodyMeasurementHandler;

        (int UserId, int IdOfMeasurement) _dom;

        [TestInitialize]
        public void SetUp()
        {
            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(true);
            AppUser dom = new AppUser("dom@gmail.com", "", "", 60, GenderType.Male, MeasurementSystem.Imperial);
            BodyMeasurement bodyMeasurement = new BodyMeasurement(
                dom,
                11,
                30,
                null,
                60,
                130,
                DateTime.Today,
                MeasurementSystem.Imperial
            );
            bodyFitTrackerContext.AppUsers.Add(dom);

            bodyFitTrackerContext.BodyMeasurements.Add(bodyMeasurement);

            bodyFitTrackerContext.SaveChanges();

            _dom = (dom.AppUserId, bodyMeasurement.AppUserId);

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            userAccessorMock.Setup(x => x.GetCurrentUserId()).Returns(dom.AppUserId);

            _createOrEditBodyMeasurementHandler = new CreateOrEditBodyMeasurementHandler(
                bodyFitTrackerContext,
                userAccessorMock.Object
            );
        }

        [TestMethod]
        // Hip circumference can not be null if gender is female
        public void HipCircumferenceAndGenderTypeIsFemaleTests()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Female);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(
                userAccessorMock.Object
            );

            validator.ShouldHaveValidationErrorFor(x => x.HipCircumference, (double?)null);
            validator.ShouldHaveValidationErrorFor(x => x.HipCircumference, (double?)0);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?)10.4);
        }

        [TestMethod]
        // Hip circumference can be null if gender is male
        public void HipCircumferenceAndGenderTypeIsMaleTests()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(
                userAccessorMock.Object
            );

            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?)null);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?)0);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?)10.4);
        }

        [TestMethod]
        public void IfCreationDateIsInTheFutureThereShouldBeAnError()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(
                userAccessorMock.Object
            );

            validator.ShouldHaveValidationErrorFor(x => x.DateAdded, DateTime.Today.AddDays(1));
        }

        [TestMethod]
        public void ValidationShouldSucceedIfAllFieldsAreValid()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(
                userAccessorMock.Object
            );
            CreateOrEditBodyMeasurementRequest createBodyMeasurement = new CreateOrEditBodyMeasurementRequest
            {
                NeckCircumference = 10,
                WaistCircumference = 10,
                HipCircumference = 10,
                Height = 60,
                Weight = 100,
                DateAdded = DateTime.Today,
            };

            ValidationResult validationResult = validator.Validate(createBodyMeasurement);
            Assert.IsTrue(validationResult.IsValid);
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
                DateAdded = DateTime.Today,
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

            Assert.ThrowsException<RestException>(
                () => _createOrEditBodyMeasurementHandler.Handle(createMeasurementRequest)
            );
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
                DateAdded = DateTime.Today,
            };

            _createOrEditBodyMeasurementHandler.Handle(createMeasurementRequest);

            BodyFitTrackerContext bodyFitTrackerContext = DatabaseConnectionFactory.GetInMemoryDatabase(false);

            double actualNewWeight = bodyFitTrackerContext.BodyMeasurements
                .Where(x => x.AppUserId == _dom.UserId)
                .First()
                .Weight;

            Assert.AreEqual(newWeight, actualNewWeight, .01);
        }
    }
}
