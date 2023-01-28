using System;
using System.Collections.Generic;

namespace Api.Domain.Models
{
    public class AppUser
    {
        public int AppUserId { get; private set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public double Height { get; set; }
        public GenderType Gender { get; private set; }
        public MeasurementSystem MeasurementSystemPreference { get; set; }
        public virtual ICollection<BodyMeasurement> BodyMeasurements { get; private set; }

        protected AppUser() {}

        public AppUser(string email, string hashedPassword, string salt, double height, GenderType gender, MeasurementSystem measurementSystemPreference)
        {
            Email = email;
            HashedPassword = hashedPassword;
            Salt = salt;
            Gender = gender;
            Height = height;
            MeasurementSystemPreference = measurementSystemPreference;
        }
    }
}