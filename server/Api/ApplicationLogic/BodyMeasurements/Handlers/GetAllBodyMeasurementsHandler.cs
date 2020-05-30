using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using Api.Persistence;
using AutoMapper;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{
   public class GetAllBodyMeasurementsHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetAllBodyMeasurementsHandler(BodyFitTrackerContext bodyFitTrackerContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Returns a <see cref="BodyMeasurementCollection"/> for the current user
        /// </summary>
        public BodyMeasurementCollection Handle()
        {
            int userId = _userAccessor.GetCurrentUserId();
            AppUser currentUser = _bodyFitTrackerContext.AppUsers.Find(userId);
            List<BodyMeasurement> bodyMeasurements = _bodyFitTrackerContext.BodyMeasurements.Where(b => b.AppUserId == userId)
                .OrderByDescending(b => b.DateAdded).ToList();

            List<BodyMeasurementDTO> bodyMeasurementDTOs = _mapper.Map<List<BodyMeasurement>, List<BodyMeasurementDTO>>(bodyMeasurements);
            return new BodyMeasurementCollection(MeasurementSystem.Imperial, currentUser.Gender, bodyMeasurementDTOs);
        }
    }
}