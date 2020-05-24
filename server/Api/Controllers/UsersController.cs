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
        public void CreateUser(CreateUserRequest createUserRequest){
            System.Console.WriteLine(createUserRequest.Email);
            System.Console.WriteLine(createUserRequest.Height);
        }
    }
}