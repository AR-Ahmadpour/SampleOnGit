using FluentValidation;

namespace Accreditation.Application.AccreditationInstances.Add;

public class AddAccreditationalInstanceCommandValidator : AbstractValidator<AddAccreditationalInstanceCommand>
{
    public AddAccreditationalInstanceCommandValidator()
    {

        RuleFor(c => c.ToDate)
            .GreaterThanOrEqualTo(r => r.FromDate);
    }
}
