using System;
using System.Linq;
using System.Text;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using FluentValidation;

namespace Api.Controllers.Users.Features;

public class ResetPasswordStepOneRequest
{
    public string Email { get; set; }
}

public class ResetPasswordStepOneRequestValidator : AbstractValidator<ResetPasswordStepOneRequest>
{
    public ResetPasswordStepOneRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull();
    }
}

public class ResetPasswordStepOneHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordResetTokenGenerator _passwordResetTokenGenerator;

    public ResetPasswordStepOneHandler(
        BodyFitTrackerContext bodyFitTrackerContext,
        IEmailSender emailSender,
        IPasswordResetTokenGenerator passwordResetTokenGenerator
    )
    {
        this._bodyFitTrackerContext = bodyFitTrackerContext;
        this._emailSender = emailSender;
        this._passwordResetTokenGenerator = passwordResetTokenGenerator;
    }

    public void Handle(ResetPasswordStepOneRequest resetPasswordStepOneRequest)
    {
        AppUser appUser = _bodyFitTrackerContext.AppUsers
            .Where(x => x.Email == resetPasswordStepOneRequest.Email)
            .FirstOrDefault();
        if (appUser == null)
            return;

        string resetToken = _passwordResetTokenGenerator.CreateResetToken();
        DateTime expiration = DateTime.Now.AddHours(1);
        PasswordReset passwordReset = new PasswordReset(resetToken, appUser.AppUserId, expiration);

        EmailMessage emailMessage = CreateEmailMessage(appUser, resetToken);
        _emailSender.SendEmail(emailMessage);

        _bodyFitTrackerContext.PasswordResets.Add(passwordReset);
        _bodyFitTrackerContext.SaveChanges();
    }

    private EmailMessage CreateEmailMessage(AppUser appUser, string resetToken)
    {
        EmailMessage emailMessage = new EmailMessage(appUser.Email, "Reset Your Password");
        StringBuilder htmlBody = new StringBuilder();
        string clientAppUrl = DotNetEnv.Env.GetString("ClientAppUrl");
        htmlBody.Append("<p>Hi, a request was received to reset your password.<p>");
        htmlBody.Append(
            $"<p>Please follow <a href='{clientAppUrl}/reset-password-step-two/{resetToken}' target='_blank'>this link</a> to reset your password."
        );
        emailMessage.HtmlBody = htmlBody.ToString();

        return emailMessage;
    }
}
