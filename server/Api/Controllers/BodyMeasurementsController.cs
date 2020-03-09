using System.Collections.Generic;
using Api.ApplicationLogic.BodyMeasurements.QueryHandlers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BodyMeasurementsController
    {
        private readonly GetAllBodyMeasurementsHandler _getAllBodyMeasurementsHandler;

        public BodyMeasurementsController(GetAllBodyMeasurementsHandler getAllBodyMeasurementsHandler)
        {
            _getAllBodyMeasurementsHandler = getAllBodyMeasurementsHandler;
        }

        [HttpGet]
        public IEnumerable<BodyMeasurement> Get()
        {
            return _getAllBodyMeasurementsHandler.Handle();
        }
    }
}