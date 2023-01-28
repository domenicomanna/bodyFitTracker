using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Persistence;
using AutoMapper;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{
    public class GetAllBodyMeasurementsHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetAllBodyMeasurementsHandler(
            BodyFitTrackerContext bodyFitTrackerContext,
            IMapper mapper,
            IUserAccessor userAccessor
        )
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Returns a list of <see cref="BodyMeasurementDTO"/> for the current user
        /// </summary>
        public List<BodyMeasurementDTO> Handle()
        {
            int userId = _userAccessor.GetCurrentUserId();
            AppUser currentUser = _bodyFitTrackerContext.AppUsers.Find(userId);

            // all measurements in the database are in imperial units
            List<BodyMeasurement> bodyMeasurements = BodyMeasurementConverter.Convert(
                currentUser.BodyMeasurements.ToList(),
                MeasurementSystem.Imperial,
                currentUser.MeasurementSystemPreference
            );

            bodyMeasurements = bodyMeasurements.OrderByDescending(b => b.DateAdded).ToList();

            List<BodyMeasurementDTO> bodyMeasurementDTOs = _mapper.Map<List<BodyMeasurement>, List<BodyMeasurementDTO>>(
                bodyMeasurements
            );
            return bodyMeasurementDTOs;
        }
    }
}
