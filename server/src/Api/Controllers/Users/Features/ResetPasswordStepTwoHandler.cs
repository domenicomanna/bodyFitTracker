using System.Linq;
using Api.Common.Interfaces;
using Api.Common.ValidationRules;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using FluentValidation;

namespace Api.Controllers.Users.Features;

public class ResetPasswordStepTwoRequest
{
    public string ResetPasswordToken { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmedNewPassword { get; set; }
}

public class ResetPasswordStepTwoRequestValidator : AbstractValidator<ResetPasswordStepTwoRequest>
{
    public ResetPasswordStepTwoRequestValidator()
    {
        RuleFor(x => x.ResetPasswordToken).NotEmpty().NotNull();
        RuleFor(x => x.NewPassword).Password();
        RuleFor(x => x.ConfirmedNewPassword).Equal(x => x.NewPassword);
    }
}

public class ResetPasswordStepTwoResult
{
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }

    public ResetPasswordStepTwoResult(bool succeeded, string errorMessage = "")
    {
        Succeeded = succeeded;
        ErrorMessage = errorMessage;
    }
}

public class ResetPasswordStepTwoHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IPasswordHasher _passwordHasher;

    public ResetPasswordStepTwoHandler(BodyFitTrackerContext bodyFitTrackerContext, IPasswordHasher passwordHasher)
    {
        _bodyFitTrackerContext = bodyFitTrackerContext;
        _passwordHasher = passwordHasher;
    }

    public ResetPasswordStepTwoResult Handle(ResetPasswordStepTwoRequest resetPasswordStepTwoRequest)
    {
        ValidateResetPasswordTokenHandler validateResetPasswordTokenHandler = new ValidateResetPasswordTokenHandler(
            _bodyFitTrackerContext
        );
        ResetPasswordValidationResult validationResult = validateResetPasswordTokenHandler.Handle(
            resetPasswordStepTwoRequest.ResetPasswordToken
        );
        if (!validationResult.Succeeded)
            return new ResetPasswordStepTwoResult(false, validationResult.ErrorMessage);

        PasswordReset passwordReset = _bodyFitTrackerContext.PasswordResets
            .Where(x => x.Token == resetPasswordStepTwoRequest.ResetPasswordToken)
            .First();
        AppUser appUser = passwordReset.AppUser;
        (string hashedPassword, string salt) = _passwordHasher.GeneratePassword(
            resetPasswordStepTwoRequest.NewPassword
        );

        appUser.HashedPassword = hashedPassword;
        appUser.Salt = salt;

        _bodyFitTrackerContext.PasswordResets.Remove(passwordReset);
        _bodyFitTrackerContext.SaveChanges();

        return new ResetPasswordStepTwoResult(true);
    }
}
