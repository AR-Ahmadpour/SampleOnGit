using FluentValidation;


namespace Accreditation.Application.ZirMehvars.Edit;

internal sealed class EditZirMehvarCommandValidator:AbstractValidator<EditZirMehvarCommand>
{
    public EditZirMehvarCommandValidator()
    {

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
