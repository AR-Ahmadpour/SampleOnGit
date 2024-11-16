using FluentValidation;

namespace Accreditation.Application.Mehvars.Add
{
    internal sealed class AddMehvarCommandValidator : AbstractValidator<AddMehvarCommand>
    {
        public AddMehvarCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(300)
                .WithName("عنوان");

            RuleFor(x => x.EtebarDorehGUID)
                .NotEmpty()
                .WithName("دوره اعتبار بخشی");

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
