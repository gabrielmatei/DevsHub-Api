using DevsHub.Contracts.V1.Requests;
using DevsHub.Extensions;
using DevsHub.Helpers;
using FluentValidation;

namespace DevsHub.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .MinimumLength(3).WithMessage(ValidationErrors.MinimumLength)
                .MaximumLength(150).WithMessage(ValidationErrors.MaximumLength);
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .MinimumLength(3).WithMessage(ValidationErrors.MinimumLength)
                .MaximumLength(150).WithMessage(ValidationErrors.MaximumLength);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .EmailAddress().WithMessage(ValidationErrors.EmailAddress);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .Password();
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .Equal(x => x.Password).WithMessage(ValidationErrors.ConfirmPassword);
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .EmailAddress().WithMessage(ValidationErrors.EmailAddress);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty);
        }
    }
}
