using DevsHub.Contracts.V1.Requests;
using DevsHub.Helpers;
using FluentValidation;
using System;

namespace DevsHub.Validators
{
    public class CreateContestRequestValidator : AbstractValidator<CreateOrUpdateContestRequest>
    {
        public CreateContestRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
               .MinimumLength(3).WithMessage(ValidationErrors.MinimumLength)
               .MaximumLength(200).WithMessage(ValidationErrors.MaximumLength);
            RuleFor(x => x.Start)
                .NotNull().WithMessage(ValidationErrors.NotNull)
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(ValidationErrors.GreaterThanNow);
            RuleFor(x => x.End)
                .NotNull()
                .GreaterThanOrEqualTo(x => x.Start).WithMessage(ValidationErrors.GreaterThanStart);
        }
    }
}
