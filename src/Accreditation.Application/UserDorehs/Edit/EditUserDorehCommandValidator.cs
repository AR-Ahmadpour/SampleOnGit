using FluentValidation;

namespace Accreditation.Application.UserDorehs.Edit
{
    internal sealed class EditUserDorehCommandValidator : AbstractValidator<EditUserDorehCommand>
    {
        public EditUserDorehCommandValidator()
        {

            RuleFor(x => x.DorehAmoozeshiGuid)
                .NotEmpty();

            RuleFor(x => x.DorehTitle)
                .NotEmpty();

            RuleFor(x => x.DorehRole)
                .NotNull();

            RuleFor(x => x.BarGozarKonandeh)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.DorehHours)
                .NotEmpty();
        }
    }
}
