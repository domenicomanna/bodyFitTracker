using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using AutoMapper;
using Domain.Models;

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