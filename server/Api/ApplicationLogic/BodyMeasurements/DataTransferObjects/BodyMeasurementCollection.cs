using System.Collections.Generic;
using Api.Domain.Models;

namespace Api.ApplicationLogic.BodyMeasurements.DataTransferObjects
{
    public class BodyMeasurementCollection
    {
        public string MeasurementSystemName { get; private set; }
        public string GenderTypeName { get; private set; }
        public Measurement Length { get; private set; }
        public Measurement Weight { get; private set; }
        public IEnumerable<BodyMeasurementDTO> BodyMeasurements { get; private set; }

        public BodyMeasurementCollection(MeasurementSystem measurementSystem, GenderType genderType, IEnumerable<BodyMeasurementDTO> bodyMeasurements)
        {
            MeasurementSystemName = measurementSystem.ToString();
            GenderTypeName = genderType.ToString();
            BodyMeasurements = bodyMeasurements;

            if (measurementSystem == MeasurementSystem.Imperial)
            {
                Weight = new Measurement("Pounds", "lb");
                Length = new Measurement("Inches", "in");
            }
            else
            {
                Weight = new Measurement("Kilograms", "kg");
                Length = new Measurement("Centimeters", "cm");
            }
        }
    }
}