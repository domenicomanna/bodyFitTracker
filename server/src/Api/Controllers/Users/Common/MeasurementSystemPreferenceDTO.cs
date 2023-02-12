using Api.Domain.Models;

namespace Api.Controllers.Users.Common;

public class MeasurementSystemDTO
{
    public string MeasurementSystemName { get; private set; }
    public string LengthUnit { get; private set; }
    public string WeightUnit { get; private set; }

    public MeasurementSystemDTO(MeasurementSystem measurementSystemPreference)
    {
        MeasurementSystemName = measurementSystemPreference.ToString();
        if (measurementSystemPreference == MeasurementSystem.Imperial)
        {
            LengthUnit = "in";
            WeightUnit = "lb";
        }
        else
        {
            LengthUnit = "cm";
            WeightUnit = "kg";
        }
    }
}
