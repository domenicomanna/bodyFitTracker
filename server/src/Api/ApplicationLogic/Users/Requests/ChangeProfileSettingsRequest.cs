using Api.Domain.Models;
using FluentValidation;

namespace Api.ApplicationLogic.Users.Requests
{
    public class ChangeProfileSettingsRequest
    {
        public string Email { get; set; }
        public double Height { get; set; }
        public MeasurementSystem UnitsOfMeasure { get; set; }
    }

    public class ChangeProfileSettingsRequestValidator : AbstractValidator<ChangeProfileSettingsRequest>
    {
        public ChangeProfileSettingsRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Height).GreaterThan(0);
            RuleFor(x => x.UnitsOfMeasure).IsInEnum();
        }
    }
}
