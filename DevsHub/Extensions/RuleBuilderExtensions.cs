using DevsHub.Helpers;
using FluentValidation;

namespace DevsHub.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 14)
        {
            return ruleBuilder
                .MinimumLength(6).WithMessage(ValidationErrors.PasswordMinimumLength)
                .MaximumLength(16).WithMessage(ValidationErrors.PasswordMaximumLength)
                .Matches("[A-Z]").WithMessage(ValidationErrors.PasswordUppercaseLetter)
                .Matches("[a-z]").WithMessage(ValidationErrors.PasswordLowercaseLetter)
                .Matches("[0-9]").WithMessage(ValidationErrors.PasswordDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(ValidationErrors.PasswordSpecialCharacter);
        }
    }
}
