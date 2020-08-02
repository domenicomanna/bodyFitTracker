using System;
using System.Linq;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.Domain.Models;
using Api.Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class ValidateResetPasswordTokenHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;

        public ValidateResetPasswordTokenHandler(BodyFitTrackerContext bodyFitTrackerContext)
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
        }

        public ResetPasswordValidationResult Handle(string token)
        {
            PasswordReset passwordReset = _bodyFitTrackerContext.PasswordResets.Where(x => x.Token == token).FirstOrDefault();
            if (passwordReset == null) return new ResetPasswordValidationResult(false, "Token not found");
            if (passwordReset.Expiration < DateTime.Now) return new ResetPasswordValidationResult(false, "Token is exipred");
            
            return new ResetPasswordValidationResult(true);
        }
    }
}