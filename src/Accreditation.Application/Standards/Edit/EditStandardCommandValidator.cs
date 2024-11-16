using FluentValidation;


namespace Accreditation.Application.Standards.Edit
{
    internal sealed class EditStandardCommandValidator : AbstractValidator<EditStandardCommand>
    {
        public EditStandardCommandValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(x => x.Code)
                   .MaximumLength(50)
                   .NotEmpty();

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
