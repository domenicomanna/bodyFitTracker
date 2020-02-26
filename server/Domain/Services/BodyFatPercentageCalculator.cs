using System;
using Domain.Models;

namespace Domain.Services
{
    public class BodyFatPercentageCalculator
    {
        public static double CalculateBodyFatPercentage(AppUser appUser, BodyMeasurement bodyMeasurement)
        {
            if (appUser.Gender == GenderType.Male)
            {
                double maleBodyFatPercentage = 86.01 * Math.Log10(bodyMeasurement.WaistCircumference - bodyMeasurement.NeckCircumference) -
                    (70.041 * Math.Log10(appUser.Height)) + 36.76;
                    
                return maleBodyFatPercentage;
            }

            // Calculation for female
            double femaleBodyFatPercentage = 163.205 * Math.Log10(bodyMeasurement.WaistCircumference + bodyMeasurement.HipCircumference - 
                bodyMeasurement.NeckCircumference ) - (97.684 * Math.Log10(appUser.Height)) - 78.387;;

            return femaleBodyFatPercentage;
        }
    }
}