using DevsHub.Contracts.V1.Requests;
using FluentValidation;

namespace DevsHub.Validators
{
    #region Tutorials
    #endregion

    #region Categories
    public class UpdateTutorialCategoryRequestValidator : AbstractValidator<CreateOrUpdateTutorialCategoryRequest>
    {
        public UpdateTutorialCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .MaximumLength(50);
        }
    }
    #endregion
}
