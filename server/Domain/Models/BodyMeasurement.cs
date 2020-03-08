using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BodyMeasurement
    {
        public int BodyMeasurementId { get; set; }

        public double NeckCircumference { get; set; }

        public double WaistCircumference { get; set; }

        public double? HipCircumference { get; set; }

        public double Weight { get; set; }

        public string AppUserEmail { get; set; }
        
        public AppUser AppUser { get; set; }
    }
}