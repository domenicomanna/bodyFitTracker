using Api.Controllers.BodyMeasurements.Common;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Controllers.BodyMeasurements;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BodyMeasurement, BodyMeasurementDTO>();
    }
}
