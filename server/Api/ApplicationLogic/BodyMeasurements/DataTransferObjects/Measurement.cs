namespace Api.ApplicationLogic.BodyMeasurements.DataTransferObjects
{
    public class Measurement
    {
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public Measurement(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}