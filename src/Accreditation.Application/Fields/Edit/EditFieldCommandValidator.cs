using FluentValidation;

namespace Accreditation.Application.Fields.Edit
{
    internal sealed  class EditFieldCommandValidator:AbstractValidator<EditFieldCommand>
    {
        public EditFieldCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.TitleCode)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.InstanceTypeIds)
                .NotEmpty();

        }
    }
}
