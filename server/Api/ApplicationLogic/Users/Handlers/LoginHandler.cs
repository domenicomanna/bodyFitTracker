using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.ApplicationLogic.Errors;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Requests;
using Api.Domain.Models;
using Api.Infrastructure.PasswordHashing;
using Api.Infrastructure.Security;
using Api.Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class LoginHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        public LoginHandler(BodyFitTrackerContext bodyFitTrackerContext, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
        {

            _bodyFitTrackerContext = bodyFitTrackerContext;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        /// <summary>
        /// Attempts to log in the user based off of the credentials in <paramref name="loginRequest"/>. If the credentials are invalid
        /// a <see cref="RestException"/> will be thrown.
        /// </summary>
        public AppUserDTO Handle(LoginRequest loginRequest)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.Email == loginRequest.Email).FirstOrDefault();

            if (appUser == null)
            {
                errors.Add("", "Invalid username or password");
                throw new RestException(HttpStatusCode.Unauthorized, errors);
            }

            bool passwordIsValid = _passwordHasher.ValidatePlainTextPassword(loginRequest.Password, appUser.HashedPassword, appUser.Salt);

            if (!passwordIsValid)
            {
                errors.Add("", "Invalid username or password");
                throw new RestException(HttpStatusCode.Unauthorized, errors);
            }

            return new AppUserDTO
            {
                Email = appUser.Email,
                Token = _jwtGenerator.CreateToken(appUser)
            };
        }
    }
}