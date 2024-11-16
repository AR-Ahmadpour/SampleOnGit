

using FluentValidation;

namespace Accreditation.Application.Mehvars.Edit
{
    internal class EditMehvarCommandValidator:AbstractValidator<EditMehvarCommand>
    {
        public EditMehvarCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(300)
                .WithName("عنوان");

            RuleFor(x => x.WeightedCoefficient)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithName("ضریب وزنی");

            RuleFor(x => x.SortOrder)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithName("ترتیب");
        }
    }
}
