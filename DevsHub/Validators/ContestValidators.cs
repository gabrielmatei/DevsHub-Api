using DevsHub.Contracts.V1.Requests;
using FluentValidation;
using System;

namespace DevsHub.Validators
{
    public class CreateContestRequestValidator : AbstractValidator<CreateOrUpdateContestRequest>
    {
        public CreateContestRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(200);
            RuleFor(x => x.Start)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(x => x.End)
                .NotNull()
                .GreaterThanOrEqualTo(x => x.Start);
        }
    }
}
