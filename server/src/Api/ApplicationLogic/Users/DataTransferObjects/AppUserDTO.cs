namespace Api.ApplicationLogic.Users.DataTransferObjects
{
    public class AppUserDTO
    {
        public string Email { get; set; }
        public string Gender { get; set; }
        public double Height { get; set; }
        public MeasurementSystemDTO MeasurementPreference { get; set; }
    }
}