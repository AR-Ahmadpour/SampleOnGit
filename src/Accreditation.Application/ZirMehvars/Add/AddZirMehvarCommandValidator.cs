using FluentValidation;

namespace Accreditation.Application.ZirMehvars.Add
{
    internal sealed class AddZirMehvarCommandValidator : AbstractValidator<AddZirMehvarCommand>
    {
        public AddZirMehvarCommandValidator()
        {
            RuleFor(x => x.MehvarGuid)
               .NotEmpty()
               .WithName("شناسه محور");

            RuleFor(x => x.Title)
               .NotEmpty()
               .MaximumLength(200)
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
