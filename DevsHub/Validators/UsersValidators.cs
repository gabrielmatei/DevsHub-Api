using DevsHub.Contracts.V1.Requests;
using FluentValidation;

namespace DevsHub.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            // TODO validate role
        }
    }
}
