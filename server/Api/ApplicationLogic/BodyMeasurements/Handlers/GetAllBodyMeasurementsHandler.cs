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
            string currentUser = "abc@gmail.com";

            List<BodyMeasurement> measurements = _dataContext.BodyMeasurements.Where(b => b.AppUserEmail == currentUser).ToList();
            List<BodyMeasurementDTO> measurementsToReturn = _mapper.Map<List<BodyMeasurement>, List<BodyMeasurementDTO>>(measurements);
            return new BodyMeasurementCollection(MeasurementSystem.Imperial, measurementsToReturn);

        }
    }
}