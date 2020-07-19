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

        public UsersController(GetUserHandler getUserHandler, CreateUserHandler createUserHandler)
        {
            _getUserHandler = getUserHandler;
            _createUserHandler = createUserHandler;
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
    }
}