namespace Api.ApplicationLogic.Users.DataTransferObjects
{
    public class AppUserDTO
    {
        public string Email { get; set; }
        public string Gender { get; set; }
        public MeasurementSystemDTO MeasurementSystemPreference { get; set; }
        public string Token { get; set; }
    }
}