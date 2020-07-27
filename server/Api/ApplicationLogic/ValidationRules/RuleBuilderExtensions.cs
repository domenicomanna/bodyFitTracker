using FluentValidation;

namespace Api.ApplicationLogic.ValidationRules
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                          .NotEmpty()
                          .NotNull()
                          .MinimumLength(4);

            return options;
        }
    }
}