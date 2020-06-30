using System;

namespace Api.ApplicationLogic.BodyMeasurements.DataTransferObjects
{
   public class BodyMeasurementDTO
    {
        public int BodyMeasurementId { get; set; }

        public double NeckCircumference { get; set; }

        public double WaistCircumference { get; set; }

        public double? HipCircumference { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public double BodyFatPercentage { get; set; }
        
        public DateTime DateAdded { get; set; }

        public bool ShouldSerializeHipCircumference() => HipCircumference != null;
    }
}