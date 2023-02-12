using System.Collections.Generic;
using Api.Domain.Models;

namespace Api.Domain.Services
{
    public class BodyMeasurementConverter
    {
        public static List<BodyMeasurement> Convert(
            List<BodyMeasurement> bodyMeasurements,
            MeasurementSystem sourceUnits,
            MeasurementSystem destinationUnits
        )
        {
            if (sourceUnits == destinationUnits)
                return bodyMeasurements;
            List<BodyMeasurement> convertedBodyMeasurements = new List<BodyMeasurement>();
            foreach (BodyMeasurement bodyMeasurement in bodyMeasurements)
            {
                convertedBodyMeasurements.Add(Convert(bodyMeasurement, sourceUnits, destinationUnits));
            }
            return convertedBodyMeasurements;
        }

        public static BodyMeasurement Convert(
            BodyMeasurement bodyMeasurement,
            MeasurementSystem sourceUnits,
            MeasurementSystem destinationUnits
        )
        {
            if (sourceUnits == destinationUnits)
                return bodyMeasurement;

            double? hipCircumference = null;
            double neckCircumference = MeasurementConverter.ConvertLength(
                bodyMeasurement.NeckCircumference,
                sourceUnits,
                destinationUnits
            );
            double waistCircumference = MeasurementConverter.ConvertLength(
                bodyMeasurement.WaistCircumference,
                sourceUnits,
                destinationUnits
            );
            if (bodyMeasurement.HipCircumference.HasValue)
            {
                hipCircumference = MeasurementConverter.ConvertLength(
                    (double)bodyMeasurement.HipCircumference,
                    sourceUnits,
                    destinationUnits
                );
            }

            double height = MeasurementConverter.ConvertLength(bodyMeasurement.Height, sourceUnits, destinationUnits);
            double weight = MeasurementConverter.ConvertWeight(bodyMeasurement.Weight, sourceUnits, destinationUnits);

            return new BodyMeasurement(
                bodyMeasurement.AppUser,
                neckCircumference,
                waistCircumference,
                hipCircumference,
                height,
                weight,
                bodyMeasurement.DateAdded,
                destinationUnits
            )
            {
                BodyMeasurementId = bodyMeasurement.BodyMeasurementId
            };
        }
    }
}
