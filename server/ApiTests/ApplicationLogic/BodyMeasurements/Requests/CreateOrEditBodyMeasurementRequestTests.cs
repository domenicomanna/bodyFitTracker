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
    public class CreateOrEditBodyMeasurementRequestTests
    {

        [TestMethod]
        // Hip circumference can not be null if gender is female
        public void HipCircumferenceAndGenderTypeIsFemaleTests()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Female);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(userAccessorMock.Object);

            validator.ShouldHaveValidationErrorFor(x => x.HipCircumference, (double?) null);
            validator.ShouldHaveValidationErrorFor(x => x.HipCircumference, (double?) 0);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?) 10.4);
        }

        [TestMethod]
        // Hip circumference can be null if gender is male
        public void HipCircumferenceAndGenderTypeIsMaleTests()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(userAccessorMock.Object);

            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?) null);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?) 0);
            validator.ShouldNotHaveValidationErrorFor(x => x.HipCircumference, (double?) 10.4);
        }

        [TestMethod]
        public void IfCreationDateIsInTheFutureThereShouldBeAnError()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(userAccessorMock.Object);

            validator.ShouldHaveValidationErrorFor(x => x.CreationDate, DateTime.Today.AddDays(1));
        }

        [TestMethod]
        public void ValidationShouldSucceedIfAllFieldsAreValid()
        {
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.GetCurrentUsersGender()).Returns(GenderType.Male);
            CreateOrEditBodyMeasurementRequestValidator validator = new CreateOrEditBodyMeasurementRequestValidator(userAccessorMock.Object);
            CreateOrEditBodyMeasurementRequest createBodyMeasurement = new CreateOrEditBodyMeasurementRequest
            {
                NeckCircumference = 10,
                WaistCircumference = 10,
                HipCircumference = 10,
                Height = 60,
                Weight = 100,
                CreationDate = DateTime.Today,
            };

            ValidationResult validationResult = validator.Validate(createBodyMeasurement);
            Assert.IsTrue(validationResult.IsValid);
        }
    }
}