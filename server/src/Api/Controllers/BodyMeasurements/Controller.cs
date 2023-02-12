using System.Collections.Generic;
using Api.Controllers.BodyMeasurements.Common;
using Api.Controllers.BodyMeasurements.Features;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BodyMeasurements
{
    [ApiController]
    [Route("api/[controller]")]
    public class BodyMeasurementsController
    {
        [HttpGet("")]
        public List<BodyMeasurementDTO> GetAllBodyMeasurements([FromServices] GetAllBodyMeasurementsHandler handler)
        {
            return handler.Handle();
        }

        [HttpGet("{id}")]
        public BodyMeasurementDTO GetBodyMeasurement([FromServices] GetBodyMeasurementHandler handler, int id)
        {
            return handler.Handle(id);
        }

        [HttpPost("")]
        public void CreateBodyMeasurement(
            [FromServices] CreateOrEditBodyMeasurementHandler handler,
            CreateOrEditBodyMeasurementRequest createBodyMeasurementRequest
        )
        {
            handler.Handle(createBodyMeasurementRequest);
        }

        [HttpPut("{id}")]
        public void EditBodyMeasurement(
            [FromServices] CreateOrEditBodyMeasurementHandler handler,
            int id,
            CreateOrEditBodyMeasurementRequest editBodyMeasurementRequest
        )
        {
            editBodyMeasurementRequest.IdOfBodyMeasurementToEdit = id;
            handler.Handle(editBodyMeasurementRequest);
        }

        [HttpDelete("{id}")]
        public void Delete([FromServices] DeleteBodyMeasurementHandler handler, int id)
        {
            handler.Handle(id);
        }
    }
}
