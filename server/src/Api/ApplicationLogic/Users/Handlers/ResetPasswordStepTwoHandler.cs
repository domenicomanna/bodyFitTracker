using System.Linq;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Requests;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
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
            ValidateResetPasswordTokenHandler validateResetPasswordTokenHandler = new ValidateResetPasswordTokenHandler(_bodyFitTrackerContext);
            ResetPasswordValidationResult validationResult = validateResetPasswordTokenHandler.Handle(resetPasswordStepTwoRequest.ResetPasswordToken);
            if (!validationResult.Succeeded) return new ResetPasswordStepTwoResult(false, validationResult.ErrorMessage);

            PasswordReset passwordReset = _bodyFitTrackerContext.PasswordResets
                .Where(x => x.Token == resetPasswordStepTwoRequest.ResetPasswordToken).First();
            AppUser appUser = passwordReset.AppUser;
            (string hashedPassword, string salt) = _passwordHasher.GeneratePassword(resetPasswordStepTwoRequest.NewPassword);

            appUser.HashedPassword = hashedPassword;
            appUser.Salt = salt;

            _bodyFitTrackerContext.PasswordResets.Remove(passwordReset);
            _bodyFitTrackerContext.SaveChanges();

            return new ResetPasswordStepTwoResult(true);

        }
    }
}