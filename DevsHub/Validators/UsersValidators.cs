using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using DevsHub.Helpers;
using FluentValidation;
using System.Collections.Generic;

namespace DevsHub.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Role)
                .Must(r => new List<string>(Role.Roles).Contains(r)).WithMessage(ValidationErrors.IsRole);
        }
    }
}
