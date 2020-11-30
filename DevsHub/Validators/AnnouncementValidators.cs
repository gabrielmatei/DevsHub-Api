using DevsHub.Contracts.V1.Requests;
using FluentValidation;

namespace DevsHub.Validators
{
    public class CreateOrUpdateAnnouncementRequestValidator : AbstractValidator<CreateOrUpdateAnnouncementRequest>
    {
        public CreateOrUpdateAnnouncementRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Body)
                .NotEmpty();
            RuleFor(x => x.Type)
                .IsEnumName(typeof(Data.AnnouncementType));
            RuleFor(x => x.Start)
                .NotNull();
            RuleFor(x => x.End)
                .NotNull()
                .GreaterThanOrEqualTo(x => x.Start);
        }
    }
}
