using FluentValidation;


namespace Accreditation.Application.UserDorehs.Add
{
    internal sealed class AddUserDorehCommandValidator:AbstractValidator<AddUserDorehCommand>
    {
        public AddUserDorehCommandValidator()
        {
            RuleFor(x => x.UserGuid)
                .NotEmpty();

            RuleFor(x => x.DorehAmoozeshiGuid)
                .NotEmpty();

            RuleFor(x=>x.DorehTitle)
                .NotEmpty();

            RuleFor(x=>x.DorehRole)
                .NotNull();

            RuleFor(x => x.BargozarKonandeh)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x=>x.DorehHours)
                .NotEmpty();
        }
    }
}
