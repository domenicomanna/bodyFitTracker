using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Persistence;

namespace Api.ApplicationLogic.BodyMeasurements.QueryHandlers
{
    public class GetAllBodyMeasurementsHandler
    {
        private readonly DataContext _dataContext;
        public GetAllBodyMeasurementsHandler(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public IEnumerable<BodyMeasurement> Handle()
        {
            string currentUser = "abc@gmail.com";
            IEnumerable<BodyMeasurement> measurements = _dataContext.BodyMeasurements.Where(b => b.AppUserEmail == currentUser);
            return measurements;
        }
    }
}