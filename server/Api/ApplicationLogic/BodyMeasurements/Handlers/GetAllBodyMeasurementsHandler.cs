using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.Domain.Models;
using Api.Persistence;
using AutoMapper;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{
   public class GetAllBodyMeasurementsHandler
    {
        private readonly BodyFitTrackerContext _dataContext;
        private readonly IMapper _mapper;

        public GetAllBodyMeasurementsHandler(BodyFitTrackerContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;

        }

        public BodyMeasurementCollection Handle()
        {
            string currentUserEmail = "abc@gmail.com";
            AppUser currentUser = _dataContext.AppUsers.Find(currentUserEmail);
            List<BodyMeasurement> bodyMeasurements = _dataContext.BodyMeasurements.Where(b => b.AppUserEmail == currentUserEmail)
                .OrderByDescending(b => b.DateAdded).ToList();

            List<BodyMeasurementDTO> bodyMeasurementDTOs = _mapper.Map<List<BodyMeasurement>, List<BodyMeasurementDTO>>(bodyMeasurements);
            return new BodyMeasurementCollection(MeasurementSystem.Imperial, currentUser.Gender, bodyMeasurementDTOs);
        }
    }
}