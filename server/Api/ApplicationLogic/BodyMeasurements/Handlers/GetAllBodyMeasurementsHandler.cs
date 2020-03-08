using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.ApplicationLogic.BodyMeasurements.Queries;
using Domain.Models;
using MediatR;
using Persistence;

namespace Api.ApplicationLogic.BodyMeasurements.Handlers
{

    public class GetAllBodyMeasurementsHandler : IRequestHandler<GetAllBodyMeasurementsQuery, IEnumerable<BodyMeasurement>>
    {
        private readonly DataContext _dataContext;
        public GetAllBodyMeasurementsHandler(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public Task<IEnumerable<BodyMeasurement>> Handle(GetAllBodyMeasurementsQuery request, CancellationToken cancellationToken)
        {
            string currentUser = "abc@gmail.com";
            IEnumerable<BodyMeasurement> measurements = _dataContext.BodyMeasurements.Where(b => b.AppUserEmail == currentUser);
            return Task.FromResult(measurements);
        }
    }
}