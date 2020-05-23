using Domain.Models;

namespace Api.ApplicationLogic.Users.Requests
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public GenderType Gender { get; set; }
        public MeasurementSystem MeasurementPreference { get; set; }
    }
}