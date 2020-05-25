using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController
    {
        private readonly CreateUserHandler _createUserHandler;

        public UsersController(CreateUserHandler createUserHandler)
        {
            _createUserHandler = createUserHandler;
        }

        [HttpPost("")]
        public CreateUserResult CreateUser(CreateUserRequest createUserRequest){
            CreateUserResult createUserResult = _createUserHandler.Handle(createUserRequest);
            return createUserResult;
        }
    }
}