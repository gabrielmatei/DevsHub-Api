using DevsHub.Contracts.V1.Requests;
using DevsHub.Helpers;
using FluentValidation;

namespace DevsHub.Validators
{
    #region Tutorials
    public class CreateOrUpdateTutorialRequestValidator : AbstractValidator<CreateOrUpdateTutorialRequest>
    {
        public CreateOrUpdateTutorialRequestValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
              .MaximumLength(150).WithMessage(ValidationErrors.MaximumLength);
            RuleFor(x => x.Content)
              .NotEmpty().WithMessage(ValidationErrors.NotEmpty);
        }
    }
    #endregion

    #region Categories
    public class UpdateTutorialCategoryRequestValidator : AbstractValidator<CreateOrUpdateTutorialCategoryRequest>
    {
        public UpdateTutorialCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
              .MaximumLength(150).WithMessage(ValidationErrors.MaximumLength);
        }
    }
    #endregion
}
