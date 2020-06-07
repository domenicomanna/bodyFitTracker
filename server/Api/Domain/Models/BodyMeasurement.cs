using System;

namespace Api.Domain.Models
{
    public class BodyMeasurement
    {
        public int BodyMeasurementId { get; private set; }

        public int AppUserId { get; private set; }

        public virtual AppUser AppUser { get; private set; }

        public double NeckCircumference { get; private set; }

        public double WaistCircumference { get; private set; }

        public double? HipCircumference { get; private set; }

        public double Weight { get; private set; }

        public DateTime DateAdded { get; private set; }

        public double BodyFatPercentage { get; set; }

        protected BodyMeasurement() { }

        public BodyMeasurement(AppUser appUser, double neckCircumference, double waistCircumference, double? hipCircumference, double weight, DateTime dateAdded)
        {
            if (appUser == null) throw new ArgumentNullException(nameof(appUser));

            if (appUser.Gender == GenderType.Female && hipCircumference == null)
            {
                throw new ArgumentNullException($"{nameof(hipCircumference)} can't be null for {nameof(GenderType.Female)}");
            }

            AppUser = appUser;
            NeckCircumference = neckCircumference;
            WaistCircumference = waistCircumference;
            AppUserId = appUser.AppUserId;
            Weight = weight;
            DateAdded = dateAdded;
            HipCircumference = appUser.Gender == GenderType.Female ? hipCircumference : null;
        }
    }
}