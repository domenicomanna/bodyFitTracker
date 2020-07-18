using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.ApplicationLogic.BodyMeasurements.Requests;
using Api.ApplicationLogic.Errors;
using Api.ApplicationLogic.Interfaces;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Persistence;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{
    public class CreateOrEditBodyMeasurementHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IUserAccessor _userAccessor;

        public CreateOrEditBodyMeasurementHandler(BodyFitTrackerContext bodyFitTrackerContext, IUserAccessor userAccessor)
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Creates a new <see cref="BodyMeasurement"/> based off of the <paramref name="createOrEditBodyMeasurementRequest"/>, if the measurement
        /// does not already exist. If the measurement described in the request does exist, then the existing measurement will be edited.
        /// </summary>
        public void Handle(CreateOrEditBodyMeasurementRequest createOrEditBodyMeasurementRequest)
        {
            bool measurementIsBeingCreated = createOrEditBodyMeasurementRequest.IdOfBodyMeasurementToEdit == null;

            if (measurementIsBeingCreated)
            {
                BodyMeasurement bodyMeasurement = CreateBodyMeasurement(createOrEditBodyMeasurementRequest);

                _bodyFitTrackerContext.BodyMeasurements.Add(bodyMeasurement);
                _bodyFitTrackerContext.SaveChanges();
            }

            else TryEditingMeasurement(createOrEditBodyMeasurementRequest);
        }

        private BodyMeasurement CreateBodyMeasurement(CreateOrEditBodyMeasurementRequest createOrEditBodyMeasurementRequest)
        {
            int currentUserId = _userAccessor.GetCurrentUserId();
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

            BodyMeasurement bodyMeasurement = new BodyMeasurement(appUser, createOrEditBodyMeasurementRequest.NeckCircumference,
                createOrEditBodyMeasurementRequest.WaistCircumference, createOrEditBodyMeasurementRequest.HipCircumference,
                createOrEditBodyMeasurementRequest.Height, createOrEditBodyMeasurementRequest.Weight, 
                createOrEditBodyMeasurementRequest.DateAdded);

            return bodyMeasurement;

        }

        private void TryEditingMeasurement(CreateOrEditBodyMeasurementRequest createOrEditBodyMeasurementRequest)
        {
            int currentUserId = _userAccessor.GetCurrentUserId();
            Dictionary<string, string> errors = new Dictionary<string, string>();

            BodyMeasurement bodyMeasurementToEdit = _bodyFitTrackerContext.BodyMeasurements.Where(x => x.BodyMeasurementId ==
                createOrEditBodyMeasurementRequest.IdOfBodyMeasurementToEdit).FirstOrDefault();

            if (bodyMeasurementToEdit == null)
            {
                errors.Add("", $"The bodymeasurement with id {createOrEditBodyMeasurementRequest.IdOfBodyMeasurementToEdit} was not found");
                throw new RestException(HttpStatusCode.NotFound, errors);
            }

            if (currentUserId != bodyMeasurementToEdit.AppUserId)
            {
                errors.Add("", "Access to another user's body measurement is denied");
                throw new RestException(HttpStatusCode.Forbidden, errors);
            }

            bodyMeasurementToEdit.NeckCircumference = createOrEditBodyMeasurementRequest.NeckCircumference;
            bodyMeasurementToEdit.WaistCircumference = createOrEditBodyMeasurementRequest.WaistCircumference;
            bodyMeasurementToEdit.HipCircumference = createOrEditBodyMeasurementRequest.HipCircumference;
            bodyMeasurementToEdit.Height = createOrEditBodyMeasurementRequest.Height;
            bodyMeasurementToEdit.Weight = createOrEditBodyMeasurementRequest.Weight;
            bodyMeasurementToEdit.DateAdded = createOrEditBodyMeasurementRequest.DateAdded;
            bodyMeasurementToEdit.BodyFatPercentage = BodyFatPercentageCalculator.CalculateBodyFatPercentage(bodyMeasurementToEdit);

            _bodyFitTrackerContext.SaveChanges();
        }
    }
}