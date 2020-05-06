using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BodyMeasurement
    {
        public int BodyMeasurementId { get; private set; }

        public string AppUserEmail { get; private set; }

        public AppUser AppUser { get; private set; }

        public double NeckCircumference { get; private set; }

        public double WaistCircumference { get; private set; }

        public double? HipCircumference { get; private set; }

        public double Weight { get; private set; }

        public DateTime DateAdded { get; private set; }

        public double BodyFatPercentage { get; set; }

        protected BodyMeasurement() { }

        public BodyMeasurement(AppUser appUser, double neckCircumference, double waistCircumference, double? hipCircumference, DateTime dateAdded)
        {
            if (appUser == null) throw new ArgumentNullException(nameof(appUser));

            if (appUser.Gender == GenderType.Female && hipCircumference == null)
            {
                throw new ArgumentNullException($"{nameof(hipCircumference)} can't be null for {nameof(GenderType.Female)}");
            }

            AppUser = appUser;
            NeckCircumference = neckCircumference;
            WaistCircumference = waistCircumference;
            AppUserEmail = appUser.Email;
            Weight = appUser.Weight;
            DateAdded = dateAdded;
            HipCircumference = appUser.Gender == GenderType.Female ? hipCircumference : null;
        }
    }
}