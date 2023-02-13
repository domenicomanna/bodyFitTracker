using System.Collections.Generic;
using System.Linq;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using Api.Common.Interfaces;
using FluentValidation;
using Api.Common.ValidationRules;
using Api.Common.Attributes;

namespace Api.Controllers.Users.Features;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmedPassword { get; set; }
    public double Height { get; set; }
    public GenderType Gender { get; set; }
    public MeasurementSystem UnitsOfMeasure { get; set; }
}

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).Password();
        RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password);
        RuleFor(x => x.Height).GreaterThan(0);
        RuleFor(x => x.Gender).IsInEnum();
        RuleFor(x => x.UnitsOfMeasure).IsInEnum();
    }
}

public class CreateUserResult
{
    public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    public bool Succeeded { get; set; }
    public string Token { get; set; } = "";
}

[Inject]
public class CreateUserHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;

    public CreateUserHandler(
        BodyFitTrackerContext bodyFitTrackerContext,
        IPasswordHasher passwordHasher,
        IJwtGenerator jwtGenerator
    )
    {
        _bodyFitTrackerContext = bodyFitTrackerContext;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
    }

    public CreateUserResult Handle(CreateUserRequest request)
    {
        Dictionary<string, string> errors = new Dictionary<string, string>();

        bool emailIsTaken = _bodyFitTrackerContext.AppUsers.Where(a => a.Email == request.Email).Any();

        if (emailIsTaken)
        {
            errors.Add("email", "That email address is already taken");
            return new CreateUserResult { Errors = errors };
        }

        (string hashedPassword, string salt) = _passwordHasher.GeneratePassword(request.Password);

        AppUser appUser = new AppUser(
            request.Email,
            hashedPassword,
            salt,
            request.Height,
            request.Gender,
            request.UnitsOfMeasure
        );

        _bodyFitTrackerContext.AppUsers.Add(appUser);
        _bodyFitTrackerContext.SaveChanges();

        return new CreateUserResult { Succeeded = true, Token = _jwtGenerator.CreateToken(appUser) };
    }
}
