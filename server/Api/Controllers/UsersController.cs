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

        public UsersController(GetUserHandler getUserHandler, CreateUserHandler createUserHandler, ChangePasswordHandler changePasswordHandler, ResetPasswordStepOneHandler resetPasswordStepOneHandler)
        {
            _getUserHandler = getUserHandler;
            _createUserHandler = createUserHandler;
            _changePasswordHandler = changePasswordHandler;
            _resetPasswordStepOneHandler = resetPasswordStepOneHandler;
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
        [HttpPut("reset-password-step-two")]
        public void ResetPasswordStepTwo(ResetPasswordStepTwoRequest resetPasswordStepTwoRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}