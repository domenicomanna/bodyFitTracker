using Api.Domain.Models;
using FluentValidation;

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

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest> {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmedPassword).Equal(x => x.Password);
            RuleFor(x => x.Height).GreaterThan(0);
            RuleFor(x => x.Weight).GreaterThan(0);
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.MeasurementPreference).IsInEnum();
        }
    }

}