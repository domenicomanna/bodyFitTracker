using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Requests;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class ChangePasswordHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserAccessor _userAccessor;

        public ChangePasswordHandler(BodyFitTrackerContext bodyFitTrackerContext, IPasswordHasher passwordHasher, IUserAccessor userAccessor)
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

            bool oldPasswordIsCorrect = _passwordHasher.ValidatePlainTextPassword(changePasswordRequest.CurrentPassword, appUser.HashedPassword, appUser.Salt);

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
}