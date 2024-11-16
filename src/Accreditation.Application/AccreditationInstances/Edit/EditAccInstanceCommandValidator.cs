using FluentValidation;

namespace Accreditation.Application.AccreditationInstances.Edit;
internal sealed class EditAccInstanceCommandValidator : AbstractValidator<EditAccInstanceCommand>
{
    public EditAccInstanceCommandValidator()
    {
        RuleFor(c => c.ToDate)
          .NotEmpty()
          .GreaterThanOrEqualTo(r => r.FromDate);
    }
}