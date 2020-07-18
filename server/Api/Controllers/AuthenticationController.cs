using Api.ApplicationLogic.Authentication.Handlers;
using Api.ApplicationLogic.Authentication.Requests;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Api.ApplicationLogic.Authentication.DataTransferObjects.SignInResult;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController
    {
        private readonly LoginHandler _loginHandler;

        public AuthenticationController(LoginHandler loginHandler)
        {
            _loginHandler = loginHandler;

        }

        [HttpPost("")]
        public SignInResult Login(LoginRequest loginRequest)
        {
            return _loginHandler.Handle(loginRequest);
        }
    }
}