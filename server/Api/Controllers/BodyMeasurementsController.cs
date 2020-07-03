using System.Collections.Generic;
using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.ApplicationLogic.BodyMeasurements.Handlers;
using Api.ApplicationLogic.BodyMeasurements.Requests;
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
        private readonly CreateOrEditBodyMeasurementHandler _createOrEditBodyMeasurementHandler;

        public BodyMeasurementsController(GetAllBodyMeasurementsHandler getAllBodyMeasurementsHandler, DeleteBodyMeasurementHandler deleteBodyMeasurementHandler, CreateOrEditBodyMeasurementHandler createOrEditBodyMeasurementHandler)
        {
            _deleteBodyMeasurementHandler = deleteBodyMeasurementHandler;
            _getAllBodyMeasurementsHandler = getAllBodyMeasurementsHandler;
            _createOrEditBodyMeasurementHandler = createOrEditBodyMeasurementHandler;
        }

        [HttpGet("")]
        public List<BodyMeasurementDTO> Get()
        {
            return _getAllBodyMeasurementsHandler.Handle();
        }

        [HttpPost("")]
        public void Create(CreateOrEditBodyMeasurementRequest createBodyMeasurementRequest)
        {
            _createOrEditBodyMeasurementHandler.Handle(createBodyMeasurementRequest);
        }

        [HttpPut("{id}")]
        public void Edit(int id, CreateOrEditBodyMeasurementRequest editBodyMeasurementRequest)
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