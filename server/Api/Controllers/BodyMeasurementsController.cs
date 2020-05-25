using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.ApplicationLogic.BodyMeasurements.Handlers;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("")]
        public BodyMeasurementCollection Get()
        {
            return _getAllBodyMeasurementsHandler.Handle();
        }
    }
}