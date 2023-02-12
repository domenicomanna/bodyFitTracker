using Api.Controllers.Users.Common;
using Api.Controllers.Users.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController
    {
        [HttpGet("")]
        public AppUserDTO GetUser([FromServices] GetUserHandler handler)
        {
            return handler.Handle();
        }

        [AllowAnonymous]
        [HttpPost("")]
        public CreateUserResult CreateUser(
            [FromServices] CreateUserHandler handler,
            CreateUserRequest createUserRequest
        )
        {
            return handler.Handle(createUserRequest);
        }

        public void ChangeProfileSettings(
            [FromServices] ChangeProfileSettingsHandler handler,
            ChangeProfileSettingsRequest changeProfileSettingsRequest
        )
        {
            handler.Handle(changeProfileSettingsRequest);
        }

        [HttpPut("change-password")]
        public ChangePasswordResult ChangePassword(
            [FromServices] ChangePasswordHandler handler,
            ChangePasswordRequest changePasswordRequest
        )
        {
            return handler.Handle(changePasswordRequest);
        }

        [AllowAnonymous]
        [HttpPost("reset-password-step-one")]
        public void ResetPasswordStepOne(
            [FromServices] ResetPasswordStepOneHandler handler,
            ResetPasswordStepOneRequest resetPasswordStepOneRequest
        )
        {
            handler.Handle(resetPasswordStepOneRequest);
        }

        [AllowAnonymous]
        [HttpGet("validate-reset-password-token/{token}")]
        public ResetPasswordValidationResult ResetPasswordStepOne(
            [FromServices] ValidateResetPasswordTokenHandler handler,
            string token
        )
        {
            return handler.Handle(token);
        }

        [AllowAnonymous]
        [HttpPut("reset-password-step-two")]
        public ResetPasswordStepTwoResult ResetPasswordStepTwo(
            [FromServices] ResetPasswordStepTwoHandler handler,
            ResetPasswordStepTwoRequest resetPasswordStepTwoRequest
        )
        {
            return handler.Handle(resetPasswordStepTwoRequest);
        }
    }
}
