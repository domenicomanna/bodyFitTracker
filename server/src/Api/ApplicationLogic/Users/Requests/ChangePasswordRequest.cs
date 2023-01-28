using Api.ApplicationLogic.ValidationRules;
using FluentValidation;

namespace Api.ApplicationLogic.Users.Requests
{
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedNewPassword { get; set; }
    }

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.ConfirmedNewPassword).Equal(x => x.NewPassword);
        }
    }
}
