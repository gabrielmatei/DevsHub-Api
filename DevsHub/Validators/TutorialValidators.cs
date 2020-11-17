using DevsHub.Contracts.V1.Requests;
using FluentValidation;

namespace DevsHub.Validators
{
    #region Tutorials
    #endregion

    #region Categories
    public class CreateTutorialCategoryRequestValidator : AbstractValidator<CreateTutorialCategoryRequest>
    {
        public CreateTutorialCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(50);
        }
    }

    public class UpdateTutorialCategoryRequestValidator : AbstractValidator<UpdateTutorialCategoryRequest>
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
