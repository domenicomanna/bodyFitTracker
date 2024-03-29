using System;
using System.Linq;
using Api.Common.Attributes;
using Api.Domain.Models;
using Api.Database;

namespace Api.Controllers.Users.Features;

public class ResetPasswordValidationResult
{
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }

    public ResetPasswordValidationResult(bool succeeded, string errorMessage = "")
    {
        Succeeded = succeeded;
        ErrorMessage = errorMessage;
    }
}

[Inject]
public class ValidateResetPasswordTokenHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;

    public ValidateResetPasswordTokenHandler(BodyFitTrackerContext bodyFitTrackerContext)
    {
        _bodyFitTrackerContext = bodyFitTrackerContext;
    }

    public ResetPasswordValidationResult Handle(string token)
    {
        PasswordReset passwordReset = _bodyFitTrackerContext.PasswordResets
            .Where(x => x.Token == token)
            .FirstOrDefault();
        if (passwordReset == null)
            return new ResetPasswordValidationResult(false, "Token not found");
        if (passwordReset.Expiration < DateTime.Now)
            return new ResetPasswordValidationResult(false, "Token is exipred");

        return new ResetPasswordValidationResult(true);
    }
}
