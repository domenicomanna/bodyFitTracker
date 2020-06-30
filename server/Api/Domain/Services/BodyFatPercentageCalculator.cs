using System;
using Api.Domain.Models;

namespace Api.Domain.Services
{
    public class BodyFatPercentageCalculator
    {
        /// <summary>
        /// Calculates the body fat percentage based off of the given <paramref name="bodyMeasurement"/>. 
        /// Note, the units are all expected to be in inches.
        /// </summary>
        /// <returns> The body fat percentage </returns>
        public static double CalculateBodyFatPercentage(BodyMeasurement bodyMeasurement)
        {
            AppUser appUser = bodyMeasurement.AppUser;

            if (appUser.Gender == GenderType.Male)
            {
                double maleBodyFatPercentage = 86.01 * Math.Log10(bodyMeasurement.WaistCircumference - bodyMeasurement.NeckCircumference) -
                    (70.041 * Math.Log10(bodyMeasurement.Height)) + 36.76;

                return maleBodyFatPercentage;
            }

            // Calculation for female
            double hipCircumference = (double) bodyMeasurement.HipCircumference;
            double femaleBodyFatPercentage = 163.205 * Math.Log10(bodyMeasurement.WaistCircumference + hipCircumference -
                bodyMeasurement.NeckCircumference) - (97.684 * Math.Log10(bodyMeasurement.Height)) - 78.387; ;

            return femaleBodyFatPercentage;
        }
    }
}