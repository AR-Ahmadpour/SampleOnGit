using FluentValidation;

namespace Accreditation.Application.EtebarDorehs.Add;

public class AddEtebarDorehCommandValidator : AbstractValidator<AddEtebarDorehCommand>
{
    public AddEtebarDorehCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            //.WithMessage(EtebarDorehErrors.TitleIsRequired.Description)
            .MaximumLength(1000);
        //.WithMessage(EtebarDorehErrors.MaximumLengthIsNotValid.Description);

        RuleFor(c => c.StartDate)
            .NotEmpty();
        //.WithMessage(EtebarDorehErrors.StartDateIsRequired.Description);

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .WithMessage(EtebarDorehErrors.EndDateIsRequired.Description)
            .GreaterThanOrEqualTo(r => r.StartDate);
            //.WithMessage(EtebarDorehErrors.EndDateHasToBeGreaterThanStartDate.Description);

        RuleFor(c => c.SortOrder)
            .GreaterThan(0);
        //.WithMessage(EtebarDorehErrors.SortOrderHasToBeGreaterThanZero.Description);

        RuleFor(c => c.OrgTypeGUID)
            .NotEmpty();
            //.WithMessage(EtebarDorehErrors.OrgTypeIsRequired.Description);
    }
}
