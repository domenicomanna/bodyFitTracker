using Api.Controllers.Authentication.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginResult = Api.Controllers.Authentication.Features.LoginResult;

namespace Api.Controllers.Authentication
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController
    {
        [HttpPost("")]
        public LoginResult Login([FromServices] LoginHandler handler, LoginRequest loginRequest)
        {
            return handler.Handle(loginRequest);
        }
    }
}
