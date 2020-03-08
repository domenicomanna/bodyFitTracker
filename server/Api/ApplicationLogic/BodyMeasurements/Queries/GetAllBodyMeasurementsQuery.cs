using System.Collections.Generic;
using Domain.Models;
using MediatR;

namespace Api.ApplicationLogic.BodyMeasurements.Queries
{
    public class GetAllBodyMeasurementsQuery : IRequest<IEnumerable<BodyMeasurement>>
    {
        
    }
}