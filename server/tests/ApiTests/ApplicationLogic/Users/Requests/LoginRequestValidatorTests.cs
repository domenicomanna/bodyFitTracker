using Api.ApplicationLogic.Authentication.Requests;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.ApplicationLogic.Users.Requests
{
    [TestClass]
    public class LoginRequestValidatorTests
    {
        LoginRequestValidator _loginRequestValidator;

        [TestInitialize]
        public void SetUp()
        {
            _loginRequestValidator = new LoginRequestValidator();
        }

        [TestMethod]
        public void IfEmailIsNullThereShouldBeAnError()
        {
            _loginRequestValidator.ShouldHaveValidationErrorFor(x => x.Email, null as string);
        }

        [TestMethod]
        public void IfPasswordIsNullThereShouldBeAnError()
        {
            _loginRequestValidator.ShouldHaveValidationErrorFor(x => x.Password, null as string);
        }
    }
}
