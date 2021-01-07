using DevsHub.Contracts.V1.Requests;
using DevsHub.Helpers;
using FluentValidation;

namespace DevsHub.Validators
{
    public class CreateOrUpdateAnnouncementRequestValidator : AbstractValidator<CreateOrUpdateAnnouncementRequest>
    {
        public CreateOrUpdateAnnouncementRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty)
                .MaximumLength(150).WithMessage(ValidationErrors.MaximumLength);
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage(ValidationErrors.NotEmpty);
            RuleFor(x => x.Type)
                .IsEnumName(typeof(Data.AnnouncementType)).WithMessage(ValidationErrors.IsAnnouncementType);
            RuleFor(x => x.Start)
                .NotNull().WithMessage(ValidationErrors.NotNull);
            RuleFor(x => x.End)
                .NotNull().WithMessage(ValidationErrors.NotNull)
                .GreaterThanOrEqualTo(x => x.Start).WithMessage(ValidationErrors.GreaterThanStart);
        }
    }
}
