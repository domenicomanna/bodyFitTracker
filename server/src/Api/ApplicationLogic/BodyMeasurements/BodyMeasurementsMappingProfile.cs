using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.Domain.Models;
using AutoMapper;

namespace Api.ApplicationLogic.BodyMeasurements
{
    public class BodyMeasurementsMappingProfile : Profile
    {
        public BodyMeasurementsMappingProfile()
        {
            CreateMap<BodyMeasurement, BodyMeasurementDTO>();
        }
    }
}
