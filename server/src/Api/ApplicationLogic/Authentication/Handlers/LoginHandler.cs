using System.Linq;
using Api.ApplicationLogic.Authentication.DataTransferObjects;
using Api.ApplicationLogic.Authentication.Requests;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;

namespace Api.ApplicationLogic.Authentication.Handlers
{
    public class LoginHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(
            BodyFitTrackerContext bodyFitTrackerContext,
            IPasswordHasher passwordHasher,
            IJwtGenerator jwtGenerator
        )
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        /// <summary>
        /// Attempts to log in the user based off of the credentials in <paramref name="loginRequest"/>.
        /// </summary>
        public SignInResult Handle(LoginRequest loginRequest)
        {
            AppUser appUser = _bodyFitTrackerContext.AppUsers
                .Where(x => x.Email == loginRequest.Email)
                .FirstOrDefault();

            if (appUser == null)
            {
                return new SignInResult { SignInWasSuccessful = false, ErrorMessage = "Invalid username or password" };
            }

            bool passwordIsValid = _passwordHasher.ValidatePlainTextPassword(
                loginRequest.Password,
                appUser.HashedPassword,
                appUser.Salt
            );

            if (!passwordIsValid)
            {
                return new SignInResult { SignInWasSuccessful = false, ErrorMessage = "Invalid username or password" };
            }

            return new SignInResult { SignInWasSuccessful = true, Token = _jwtGenerator.CreateToken(appUser) };
        }
    }
}
