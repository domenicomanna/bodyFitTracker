using System.Collections.Generic;
using Domain.Models;
using Newtonsoft.Json;

namespace Api.ApplicationLogic.BodyMeasurements.DataTransferObjects
{
    public class BodyMeasurementCollection
    {
        [JsonIgnore]
        public MeasurementSystem MeasurementSystem { get; private set; }

        public string MeasurementSystemName { get; private set;}

        public Measurement Length {
            get {
                if (MeasurementSystem == MeasurementSystem.Imperial) {
                    return new Measurement("Inches", "in");
                }
                // MeasurementSystem must be metric
                else return new Measurement("Centimeters", "cm");
            }
        }

        public Measurement Weight {
            get {
                if (MeasurementSystem == MeasurementSystem.Imperial){
                    return new Measurement("Pounds", "lb");
                }
                // MeasurementSystem must be metric
                else return new Measurement("Kilograms", "kg");
            }
        }


        public IEnumerable<BodyMeasurementDTO> BodyMeasurements { get; private set; }

        public BodyMeasurementCollection(MeasurementSystem measurementSystem, IEnumerable<BodyMeasurementDTO> bodyMeasurements)
        {
            MeasurementSystem = measurementSystem;
            MeasurementSystemName = measurementSystem.ToString();
            BodyMeasurements = bodyMeasurements;
        }
    }
}