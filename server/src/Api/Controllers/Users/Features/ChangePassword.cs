using System.Collections.Generic;
using System.Linq;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using FluentValidation;
using Api.Common.ValidationRules;

namespace Api.Controllers.Users.Features;

public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmedNewPassword { get; set; }
}

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().NotNull();
        RuleFor(x => x.NewPassword).Password();
        RuleFor(x => x.ConfirmedNewPassword).Equal(x => x.NewPassword);
    }
}

public class ChangePasswordResult
{
    public bool Succeeded { get; set; }
    public Dictionary<string, string> Errors { get; set; }

    public ChangePasswordResult(bool succeeded)
        : this(succeeded, new Dictionary<string, string>()) { }

    public ChangePasswordResult(bool succeeded, Dictionary<string, string> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }
}

public class ChangePasswordHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAccessor _userAccessor;

    public ChangePasswordHandler(
        BodyFitTrackerContext bodyFitTrackerContext,
        IPasswordHasher passwordHasher,
        IUserAccessor userAccessor
    )
    {
        this._bodyFitTrackerContext = bodyFitTrackerContext;
        this._passwordHasher = passwordHasher;
        this._userAccessor = userAccessor;
    }

    public ChangePasswordResult Handle(ChangePasswordRequest changePasswordRequest)
    {
        Dictionary<string, string> errors = new Dictionary<string, string>();
        int userId = _userAccessor.GetCurrentUserId();
        AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == userId).First();

        bool oldPasswordIsCorrect = _passwordHasher.ValidatePlainTextPassword(
            changePasswordRequest.CurrentPassword,
            appUser.HashedPassword,
            appUser.Salt
        );

        if (!oldPasswordIsCorrect)
        {
            errors.Add("currentPassword", "The password is incorrect");
            return new ChangePasswordResult(false, errors);
        }

        (string hashedPassword, string salt) = _passwordHasher.GeneratePassword(changePasswordRequest.NewPassword);

        appUser.HashedPassword = hashedPassword;
        appUser.Salt = salt;
        _bodyFitTrackerContext.SaveChanges();

        return new ChangePasswordResult(true);
    }
}
