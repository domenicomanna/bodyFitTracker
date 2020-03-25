using System.Collections.Generic;

namespace Domain.Models
{
    public class AppUser
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public GenderType Gender { get; set; }
        public MeasurementSystem MeasurementSystemPreference { get; set; }
        public ICollection<BodyMeasurement> BodyMeasurements { get; set; }
    }
}