using DevsHub.Contracts.V1.Requests;
using FluentValidation;

namespace DevsHub.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            // TODO validate role
        }
    }

    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            // TODO validate role
        }
    }
}
