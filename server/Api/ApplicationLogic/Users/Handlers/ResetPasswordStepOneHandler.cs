using System;
using System.Linq;
using System.Text;
using Api.ApplicationLogic.Users.Requests;
using Api.Common;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class ResetPasswordStepOneHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordResetTokenGenerator _passwordResetTokenGenerator;

        public ResetPasswordStepOneHandler(BodyFitTrackerContext bodyFitTrackerContext, IEmailSender emailSender, IPasswordResetTokenGenerator passwordResetTokenGenerator)
        {
            this._bodyFitTrackerContext = bodyFitTrackerContext;
            this._emailSender = emailSender;
            this._passwordResetTokenGenerator = passwordResetTokenGenerator;
        }

        public void Handle(ResetPasswordStepOneRequest resetPasswordStepOneRequest)
        {
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.Email == resetPasswordStepOneRequest.Email).FirstOrDefault();
            if (appUser == null) return;

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
            htmlBody.Append("<p>Hi, we received a request to reset your password.<p>");
            htmlBody.Append("<p>Please follow <a href='' target='_blank'>this link</a> to reset your password.");
            emailMessage.HtmlBody = htmlBody.ToString();

            return emailMessage;
        }
    }
}