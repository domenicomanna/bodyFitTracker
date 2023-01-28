using System;

namespace Api.ApplicationLogic.BodyMeasurements.DataTransferObjects
{
    public class BodyMeasurementDTO
    {
        private double _neckCircumference;
        private double _waistCircumference;
        private double? _hipCircumference;
        private double _height;
        private double _weight;
        private double _bodyFatPercentage;

        public int BodyMeasurementId { get; set; }

        public double NeckCircumference
        {
            get { return Math.Round(_neckCircumference, 2); }
            set { _neckCircumference = value; }
        }

        public double WaistCircumference
        {
            get { return Math.Round(_waistCircumference, 2); }
            set { _waistCircumference = value; }
        }

        public double? HipCircumference
        {
            get
            {
                if (_hipCircumference == null)
                    return null;
                return Math.Round((double)_hipCircumference, 2);
            }
            set { _hipCircumference = value; }
        }

        public double Height
        {
            get { return Math.Round(_height, 2); }
            set { _height = value; }
        }

        public double Weight
        {
            get { return Math.Round(_weight, 2); }
            set { _weight = value; }
        }

        public double BodyFatPercentage
        {
            get { return Math.Round(_bodyFatPercentage, 2); }
            set { _bodyFatPercentage = value; }
        }

        public DateTime DateAdded { get; set; }

        public bool ShouldSerializeHipCircumference() => HipCircumference != null;
    }
}
