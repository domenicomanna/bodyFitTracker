using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ApplicationLogic.BodyMeasurements.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BodyMeasurementsController
    {
        private readonly IMediator _mediator;
        public BodyMeasurementsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IEnumerable<BodyMeasurement>> Get()
        {
            GetAllBodyMeasurementsQuery query = new GetAllBodyMeasurementsQuery();
            return await _mediator.Send(query);
        }
    }
}