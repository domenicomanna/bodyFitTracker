using System.Linq;
using Api.ApplicationLogic.Users.Requests;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Infrastructure.Security;
using Api.Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class ChangeProfileSettingsHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IUserAccessor _userAccessor;

        public ChangeProfileSettingsHandler(BodyFitTrackerContext bodyFitTrackerContext, IUserAccessor userAccessor)
        {
            this._bodyFitTrackerContext = bodyFitTrackerContext;
            this._userAccessor = userAccessor;
        }

        public void Handle(ChangeProfileSettingsRequest changeProfileSettingsRequest)
        {
            int currentUserId = _userAccessor.GetCurrentUserId();
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

            appUser.Email = changeProfileSettingsRequest.Email;
            // all units must be in imperial in the database
            appUser.Height = MeasurementConverter.ConvertLength(changeProfileSettingsRequest.Height, changeProfileSettingsRequest.UnitsOfMeasure, MeasurementSystem.Imperial); 
            appUser.MeasurementSystemPreference = changeProfileSettingsRequest.UnitsOfMeasure;

            _bodyFitTrackerContext.SaveChanges();
        }
    }
}