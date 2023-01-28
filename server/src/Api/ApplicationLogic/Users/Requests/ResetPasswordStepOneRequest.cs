using FluentValidation;

namespace Api.ApplicationLogic.Users.Requests
{
    public class ResetPasswordStepOneRequest
    {
        public string Email { get; set; }
    }

    public class ResetPasswordStepOneRequestValidator : AbstractValidator<ResetPasswordStepOneRequest>
    {
        public ResetPasswordStepOneRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull();
        }
    }
}