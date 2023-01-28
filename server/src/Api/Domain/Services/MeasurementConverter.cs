using Api.Domain.Models;

namespace Api.Domain.Services
{
    public class MeasurementConverter
    {
        static double _centimetersInOneInch = 2.54;
        static double _kilogramsInOnePound = .45359237;

        public static double ConvertLength(double value, MeasurementSystem source, MeasurementSystem destination)
        {
            if (source == destination)
                return value;
            if (source == MeasurementSystem.Imperial)
                return value * _centimetersInOneInch; // convert to metric
            return value / _centimetersInOneInch; // convert to imperial
        }

        public static double ConvertWeight(double value, MeasurementSystem source, MeasurementSystem destination)
        {
            if (source == destination)
                return value;
            if (source == MeasurementSystem.Imperial)
                return value * _kilogramsInOnePound; // convert to metric
            return value / _kilogramsInOnePound; // convert to imperial
        }
    }
}
