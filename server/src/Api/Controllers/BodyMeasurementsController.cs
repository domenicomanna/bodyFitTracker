using System.Collections.Generic;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.ApplicationLogic.BodyMeasurements.Handlers;
using Api.ApplicationLogic.BodyMeasurements.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BodyMeasurementsController
    {
        private readonly GetAllBodyMeasurementsHandler _getAllBodyMeasurementsHandler;
        private readonly GetBodyMeasurementHandler _getBodyMeasurementHandler;
        private readonly DeleteBodyMeasurementHandler _deleteBodyMeasurementHandler;
        private readonly CreateOrEditBodyMeasurementHandler _createOrEditBodyMeasurementHandler;

        public BodyMeasurementsController(GetAllBodyMeasurementsHandler getAllBodyMeasurementsHandler, GetBodyMeasurementHandler getBodyMeasurementHandler, DeleteBodyMeasurementHandler deleteBodyMeasurementHandler, CreateOrEditBodyMeasurementHandler createOrEditBodyMeasurementHandler)
        {
            _getAllBodyMeasurementsHandler = getAllBodyMeasurementsHandler;
            _getBodyMeasurementHandler = getBodyMeasurementHandler;
            _deleteBodyMeasurementHandler = deleteBodyMeasurementHandler;
            _createOrEditBodyMeasurementHandler = createOrEditBodyMeasurementHandler;
        }

        [HttpGet("")]
        public List<BodyMeasurementDTO> GetAllBodyMeasurements()
        {
            return _getAllBodyMeasurementsHandler.Handle();
        }


        [HttpGet("{id}")]
        public BodyMeasurementDTO GetBodyMeasurement(int id)
        {
            return _getBodyMeasurementHandler.Handle(id);
        }

        [HttpPost("")]
        public void CreateBodyMeasurement(CreateOrEditBodyMeasurementRequest createBodyMeasurementRequest)
        {
            _createOrEditBodyMeasurementHandler.Handle(createBodyMeasurementRequest);
        }

        [HttpPut("{id}")]
        public void EditBodyMeasurement(int id, CreateOrEditBodyMeasurementRequest editBodyMeasurementRequest)
        {
            editBodyMeasurementRequest.IdOfBodyMeasurementToEdit = id;
            _createOrEditBodyMeasurementHandler.Handle(editBodyMeasurementRequest);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _deleteBodyMeasurementHandler.Handle(id);
        }


    }
}