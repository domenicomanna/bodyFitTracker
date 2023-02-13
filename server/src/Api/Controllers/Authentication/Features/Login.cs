using System.Linq;
using Api.Common.Attributes;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using FluentValidation;

namespace Api.Controllers.Authentication.Features;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Password).NotNull();
    }
}

public class LoginResult
{
    public bool SignInWasSuccessful { get; set; }
    public string ErrorMessage { get; set; } = "";
    public string Token { get; set; } = "";
}

[Inject]
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
    public LoginResult Handle(LoginRequest loginRequest)
    {
        AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.Email == loginRequest.Email).FirstOrDefault();

        if (appUser == null)
        {
            return new LoginResult { SignInWasSuccessful = false, ErrorMessage = "Invalid username or password" };
        }

        bool passwordIsValid = _passwordHasher.ValidatePlainTextPassword(
            loginRequest.Password,
            appUser.HashedPassword,
            appUser.Salt
        );

        if (!passwordIsValid)
        {
            return new LoginResult { SignInWasSuccessful = false, ErrorMessage = "Invalid username or password" };
        }

        return new LoginResult { SignInWasSuccessful = true, Token = _jwtGenerator.CreateToken(appUser) };
    }
}
