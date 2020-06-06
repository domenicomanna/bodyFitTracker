using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UsersController
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly LoginHandler _loginHandler;

        public UsersController(LoginHandler loginHandler, CreateUserHandler createUserHandler)
        {
            _loginHandler = loginHandler;
            _createUserHandler = createUserHandler;
        }


        [HttpGet("")]
        public AppUserDTO Login(LoginRequest loginRequest)
        {
            return _loginHandler.Handle(loginRequest);
        }

        [HttpPost("")]
        public CreateUserResult CreateUser(CreateUserRequest createUserRequest)
        {
            return _createUserHandler.Handle(createUserRequest);
        }
    }
}