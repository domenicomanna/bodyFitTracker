using Api.ApplicationLogic.BodyMeasurements.DataTransferObjects;
using Api.Domain.Models;
using AutoMapper;

namespace Api.ApplicationLogic.BodyMeasurements
{
    public class MeasurementsMappingProfile : Profile
    {
        public MeasurementsMappingProfile()
        {
            CreateMap<BodyMeasurement, BodyMeasurementDTO>();
        }
    }
}