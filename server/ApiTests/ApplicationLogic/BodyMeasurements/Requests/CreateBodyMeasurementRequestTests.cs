using System;
using Api.ApplicationLogic.BodyMeasurements.Requests;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ApiTests.ApplicationLogic.BodyMeasurements.Requests
{
    [TestClass]
    public class CreateBodyMeasurementRequestTests
    {

        [TestMethod]
        public void HipCircumferenceAndGenderTypeIsFemaleTests()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Female);
            CreateBodyMeasurementRequestValidator validator = new CreateBodyMeasurementRequestValidator(userAccessorMock.Object);

            validator.ShouldHaveValidationErrorFor(x => x.HipCircumference, (decimal?)null);
            validator.ShouldHaveValidationErrorFor(x => x.HipCircumference, (decimal?)0);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (decimal?)10.4);
        }

        [TestMethod]
        public void HipCircumferenceAndGenderTypeIsMaleTests()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateBodyMeasurementRequestValidator validator = new CreateBodyMeasurementRequestValidator(userAccessorMock.Object);

            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (decimal?)null);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (decimal?)0);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (decimal?)10.4);
        }

        [TestMethod]
        public void IfCreationDateIsInTheFutureThereShouldBeAnError()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateBodyMeasurementRequestValidator validator = new CreateBodyMeasurementRequestValidator(userAccessorMock.Object);

            validator.ShouldHaveValidationErrorFor(x => x.CreationDate, DateTime.Today.AddDays(1));
        }

        [TestMethod]
        public void ValidationShouldSucceedIfAllFieldsAreValid()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateBodyMeasurementRequestValidator validator = new CreateBodyMeasurementRequestValidator(userAccessorMock.Object);
            CreateBodyMeasurementRequest createBodyMeasurement = new CreateBodyMeasurementRequest
            {
                NeckCircumference = 10,
                WaistCircumference = 10,
                HipCircumference = 10,
                Weight = 1,
                CreationDate = DateTime.Today,
            };

            ValidationResult validationResult = validator.Validate(createBodyMeasurement);
            Assert.IsTrue(validationResult.IsValid);
        }
    }
}