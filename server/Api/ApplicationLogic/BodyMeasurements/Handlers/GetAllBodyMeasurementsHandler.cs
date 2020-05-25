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
        private readonly BodyFitTrackerContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetAllBodyMeasurementsHandler(BodyFitTrackerContext dataContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public BodyMeasurementCollection Handle()
        {
            int userId = _userAccessor.GetCurrentUserId();
            AppUser currentUser = _dataContext.AppUsers.Find(userId);
            List<BodyMeasurement> bodyMeasurements = _dataContext.BodyMeasurements.Where(b => b.AppUserId == userId)
                .OrderByDescending(b => b.DateAdded).ToList();

            List<BodyMeasurementDTO> bodyMeasurementDTOs = _mapper.Map<List<BodyMeasurement>, List<BodyMeasurementDTO>>(bodyMeasurements);
            return new BodyMeasurementCollection(MeasurementSystem.Imperial, currentUser.Gender, bodyMeasurementDTOs);
        }
    }
}