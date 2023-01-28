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

        public CreateOrEditBodyMeasurementHandler(
            BodyFitTrackerContext bodyFitTrackerContext,
            IUserAccessor userAccessor
        )
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
            else
                TryEditingMeasurement(createOrEditBodyMeasurementRequest);
        }

        private BodyMeasurement CreateBodyMeasurement(CreateOrEditBodyMeasurementRequest request)
        {
            int currentUserId = _userAccessor.GetCurrentUserId();
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

            BodyMeasurement bodyMeasurement = new BodyMeasurement(
                appUser,
                request.NeckCircumference,
                request.WaistCircumference,
                request.HipCircumference,
                request.Height,
                request.Weight,
                request.DateAdded,
                appUser.MeasurementSystemPreference
            );

            BodyMeasurement bodyMeasurementConvertedToImperial = BodyMeasurementConverter.Convert(
                bodyMeasurement,
                appUser.MeasurementSystemPreference,
                MeasurementSystem.Imperial
            ); // all measurements in the database should be in imperial units

            return bodyMeasurementConvertedToImperial;
        }

        private void TryEditingMeasurement(CreateOrEditBodyMeasurementRequest request)
        {
            int currentUserId = _userAccessor.GetCurrentUserId();
            Dictionary<string, string> errors = new Dictionary<string, string>();
            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

            BodyMeasurement bodyMeasurementToEdit = appUser.BodyMeasurements
                .Where(x => x.BodyMeasurementId == request.IdOfBodyMeasurementToEdit)
                .FirstOrDefault();

            if (bodyMeasurementToEdit == null)
            {
                errors.Add("", $"The bodymeasurement with id {request.IdOfBodyMeasurementToEdit} was not found");
                throw new RestException(HttpStatusCode.NotFound, errors);
            }

            // all measurements in the database should be in imperial units

            MeasurementSystem sourceUnits = appUser.MeasurementSystemPreference;
            MeasurementSystem destinationUnits = MeasurementSystem.Imperial;

            bodyMeasurementToEdit.NeckCircumference = MeasurementConverter.ConvertLength(
                request.NeckCircumference,
                sourceUnits,
                destinationUnits
            );

            bodyMeasurementToEdit.WaistCircumference = MeasurementConverter.ConvertLength(
                request.WaistCircumference,
                sourceUnits,
                destinationUnits
            );

            if (request.HipCircumference.HasValue)
            {
                bodyMeasurementToEdit.HipCircumference = MeasurementConverter.ConvertLength(
                    (double)request.HipCircumference,
                    sourceUnits,
                    destinationUnits
                );
            }
            bodyMeasurementToEdit.Height = MeasurementConverter.ConvertLength(
                request.Height,
                sourceUnits,
                destinationUnits
            );

            bodyMeasurementToEdit.Weight = MeasurementConverter.ConvertWeight(
                request.Weight,
                sourceUnits,
                destinationUnits
            );

            bodyMeasurementToEdit.DateAdded = request.DateAdded;

            bodyMeasurementToEdit.BodyFatPercentage = BodyFatPercentageCalculator.CalculateBodyFatPercentage(
                bodyMeasurementToEdit
            );

            _bodyFitTrackerContext.SaveChanges();
        }
    }
}
