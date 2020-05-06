using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using AutoMapper;
using Domain.Models;
using Persistence;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{
    public class GetAllBodyMeasurementsHandler
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GetAllBodyMeasurementsHandler(DataContext dataContext, IMapper mapper)
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