using DevsHub.Contracts.V1.Requests;
using FluentValidation;

namespace DevsHub.Validators
{
    #region Tutorials
    public class CreateOrUpdateTutorialRequestValidator : AbstractValidator<CreateOrUpdateTutorialRequest>
    {
        public CreateOrUpdateTutorialRequestValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty()
              .MaximumLength(150);
            RuleFor(x => x.Content)
              .NotEmpty();
        }
    }
    #endregion

    #region Categories
    public class UpdateTutorialCategoryRequestValidator : AbstractValidator<CreateOrUpdateTutorialCategoryRequest>
    {
        public UpdateTutorialCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .MaximumLength(150);
        }
    }
    #endregion
}
