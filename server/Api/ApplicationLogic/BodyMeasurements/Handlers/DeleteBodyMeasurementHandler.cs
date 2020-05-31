using System.Linq;
using System.Net;
using Api.ApplicationLogic.Errors;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using Api.Persistence;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{
    public class DeleteBodyMeasurementHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IUserAccessor _userAccessor;
        public DeleteBodyMeasurementHandler(BodyFitTrackerContext bodyFitTrackerContext, IUserAccessor userAccessor)
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Deletes the measurement with the id <paramref name="bodyMeasurementIdToDelete"/>. If no measurement is found then a RestException will be thrown. If 
        /// the measurement being deleted does not belong to the current user, then a RestException will be thrown.
        /// </summary>
        /// <param name="bodyMeasurementIdToDelete"></param>
        public void Handle(int bodyMeasurementIdToDelete)
        {
            BodyMeasurement bodyMeasurementToRemove = _bodyFitTrackerContext.BodyMeasurements
                .Where(b => b.BodyMeasurementId == bodyMeasurementIdToDelete).FirstOrDefault();

            if (bodyMeasurementToRemove == null)
            {
                throw new RestException(HttpStatusCode.NotFound, $"The bodymeasurement with id {bodyMeasurementIdToDelete} was not found");
            }
            
            int currentUserId = _userAccessor.GetCurrentUserId();

            if (currentUserId != bodyMeasurementToRemove.AppUserId){
                throw new RestException(HttpStatusCode.Forbidden, $"Access to another user's body measurement is denied");
            }

            _bodyFitTrackerContext.BodyMeasurements.Remove(bodyMeasurementToRemove);
            _bodyFitTrackerContext.SaveChanges();
        }
    }
}