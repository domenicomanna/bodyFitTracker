using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.ApplicationLogic.BodyMeasurements.Requests;
using Api.ApplicationLogic.Errors;
using Api.Common.Interfaces;
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
                createOrEditBodyMeasurementRequest.DateAdded, appUser.MeasurementSystemPreference);

            BodyMeasurement bodyMeasurementConvertedToImperial = BodyMeasurementConverter.Convert(bodyMeasurement, appUser.MeasurementSystemPreference, MeasurementSystem.Imperial); // all measurements in the database should be in imperial units

            return bodyMeasurementConvertedToImperial;
        }

        private void TryEditingMeasurement(CreateOrEditBodyMeasurementRequest createOrEditBodyMeasurementRequest)
        {
            int currentUserId = _userAccessor.GetCurrentUserId();
            Dictionary<string, string> errors = new Dictionary<string, string>();
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

            BodyMeasurement bodyMeasurementToEdit = appUser.BodyMeasurements.Where(x => x.BodyMeasurementId ==
                createOrEditBodyMeasurementRequest.IdOfBodyMeasurementToEdit).FirstOrDefault();

            if (bodyMeasurementToEdit == null)
            {
                errors.Add("", $"The bodymeasurement with id {createOrEditBodyMeasurementRequest.IdOfBodyMeasurementToEdit} was not found");
                throw new RestException(HttpStatusCode.NotFound, errors);
            }

            // all measurements in the database should be in imperial units

            MeasurementSystem sourceUnits = appUser.MeasurementSystemPreference;
            MeasurementSystem destinationUnits = MeasurementSystem.Imperial;

            bodyMeasurementToEdit.NeckCircumference =
                MeasurementConverter.ConvertLength(createOrEditBodyMeasurementRequest.NeckCircumference, sourceUnits, destinationUnits);

            bodyMeasurementToEdit.WaistCircumference =
                MeasurementConverter.ConvertLength(createOrEditBodyMeasurementRequest.WaistCircumference,
                sourceUnits, destinationUnits);

            if (createOrEditBodyMeasurementRequest.HipCircumference.HasValue)
            {
                bodyMeasurementToEdit.HipCircumference =
                    MeasurementConverter.ConvertLength((double)createOrEditBodyMeasurementRequest.HipCircumference, sourceUnits, destinationUnits);
            }
            bodyMeasurementToEdit.Height =
                MeasurementConverter.ConvertLength(createOrEditBodyMeasurementRequest.Height, sourceUnits, destinationUnits);

            bodyMeasurementToEdit.Weight =
                MeasurementConverter.ConvertWeight(createOrEditBodyMeasurementRequest.Weight, sourceUnits, destinationUnits);

            bodyMeasurementToEdit.DateAdded = createOrEditBodyMeasurementRequest.DateAdded;

            bodyMeasurementToEdit.BodyFatPercentage = BodyFatPercentageCalculator.CalculateBodyFatPercentage(bodyMeasurementToEdit);

            _bodyFitTrackerContext.SaveChanges();
        }
    }
}