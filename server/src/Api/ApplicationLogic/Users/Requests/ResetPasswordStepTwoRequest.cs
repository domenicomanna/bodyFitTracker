using Api.ApplicationLogic.ValidationRules;
using FluentValidation;

namespace Api.ApplicationLogic.Users.Requests
{
    public class ResetPasswordStepTwoRequest
    {
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedNewPassword { get; set; }
    }

    public class ResetPasswordStepTwoRequestValidator : AbstractValidator<ResetPasswordStepTwoRequest>
    {
        public ResetPasswordStepTwoRequestValidator()
        {
            RuleFor(x => x.ResetPasswordToken).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.ConfirmedNewPassword).Equal(x => x.NewPassword);
        }
    }
}