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
        private readonly DeleteBodyMeasurementHandler _deleteBodyMeasurementHandler;

        public BodyMeasurementsController(GetAllBodyMeasurementsHandler getAllBodyMeasurementsHandler, DeleteBodyMeasurementHandler deleteBodyMeasurementHandler)
        {
            _deleteBodyMeasurementHandler = deleteBodyMeasurementHandler;
            _getAllBodyMeasurementsHandler = getAllBodyMeasurementsHandler;
        }

        [HttpGet("")]
        public BodyMeasurementCollection Get()
        {
            return _getAllBodyMeasurementsHandler.Handle();
        }

        [HttpDelete("{id}")]        
        public void Delete(int id)
        {
            _deleteBodyMeasurementHandler.Handle(id);
        }
    }
}