using System;

namespace Api.ApplicationLogic.BodyMeasurements.DataTransferObjects
{
    public class BodyMeasurementDTO
    {
        private double _bodyFatPercentage;

        public int BodyMeasurementId { get; set; }

        public double NeckCircumference { get; set; }

        public double WaistCircumference { get; set; }

        public double? HipCircumference { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public double BodyFatPercentage
        {
            get
            {
                return Math.Round(_bodyFatPercentage, 2, MidpointRounding.AwayFromZero);
            }
            set
            {
                _bodyFatPercentage = value;
            }
        }

        public DateTime DateAdded { get; set; }

        public bool ShouldSerializeHipCircumference() => HipCircumference != null;
    }
}