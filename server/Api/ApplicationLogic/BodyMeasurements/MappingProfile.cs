using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.Domain.Models;
using AutoMapper;

namespace Api.ApplicationLogic.BodyMeasurements
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BodyMeasurement, BodyMeasurementDTO>();
        }
    }
}