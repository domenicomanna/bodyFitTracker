using System.Collections.Generic;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController
    {
        private readonly GetUserHandler _getUserHandler;
        private readonly CreateUserHandler _createUserHandler;
        private readonly ChangePasswordHandler _changePasswordHandler;
        private readonly ResetPasswordStepOneHandler _resetPasswordStepOneHandler;
        private readonly ValidateResetPasswordTokenHandler _validateResetPasswordTokenHandler;
        private readonly ResetPasswordStepTwoHandler _resetPasswordStepTwoHandler;
        private readonly ChangeProfileSettingsHandler _changeProfileSettingsHandler;

        public UsersController(
            GetUserHandler getUserHandler,
            CreateUserHandler createUserHandler,
            ChangePasswordHandler changePasswordHandler,
            ResetPasswordStepOneHandler resetPasswordStepOneHandler,
            ValidateResetPasswordTokenHandler validateResetPasswordTokenHandler,
            ResetPasswordStepTwoHandler resetPasswordStepTwoHandler,
            ChangeProfileSettingsHandler changeProfileSettingsHandler
        )
        {
            _getUserHandler = getUserHandler;
            _createUserHandler = createUserHandler;
            _changePasswordHandler = changePasswordHandler;
            _resetPasswordStepOneHandler = resetPasswordStepOneHandler;
            _validateResetPasswordTokenHandler = validateResetPasswordTokenHandler;
            _resetPasswordStepTwoHandler = resetPasswordStepTwoHandler;
            _changeProfileSettingsHandler = changeProfileSettingsHandler;
        }

        [HttpGet("")]
        public AppUserDTO GetUser()
        {
            return _getUserHandler.Handle();
        }

        [AllowAnonymous]
        [HttpPost("")]
        public CreateUserResult CreateUser(CreateUserRequest createUserRequest)
        {
            return _createUserHandler.Handle(createUserRequest);
        }

        public void ChangeProfileSettings(ChangeProfileSettingsRequest changeProfileSettingsRequest)
        {
            _changeProfileSettingsHandler.Handle(changeProfileSettingsRequest);
        }

        [HttpPut("change-password")]
        public ChangePasswordResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            return _changePasswordHandler.Handle(changePasswordRequest);
        }

        [AllowAnonymous]
        [HttpPost("reset-password-step-one")]
        public void ResetPasswordStepOne(ResetPasswordStepOneRequest resetPasswordStepOneRequest)
        {
            _resetPasswordStepOneHandler.Handle(resetPasswordStepOneRequest);
        }

        [AllowAnonymous]
        [HttpGet("validate-reset-password-token/{token}")]
        public ResetPasswordValidationResult ResetPasswordStepOne(string token)
        {
            return _validateResetPasswordTokenHandler.Handle(token);
        }

        [AllowAnonymous]
        [HttpPut("reset-password-step-two")]
        public ResetPasswordStepTwoResult ResetPasswordStepTwo(ResetPasswordStepTwoRequest resetPasswordStepTwoRequest)
        {
            return _resetPasswordStepTwoHandler.Handle(resetPasswordStepTwoRequest);
        }
    }
}
