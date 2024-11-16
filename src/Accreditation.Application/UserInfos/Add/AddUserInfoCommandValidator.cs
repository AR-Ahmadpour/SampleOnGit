using FluentValidation;

namespace Accreditation.Application.UserInfos.Add
{
    internal sealed class AddUserInfoCommandValidator : AbstractValidator<AddUserInfoCommand>
    {
        public AddUserInfoCommandValidator()
        {
            RuleFor(x => x.UserGUID)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.MaritalStatus)
                .NotNull(); 

            RuleFor(x => x.ChildCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.BirthOstandId)
               .NotEmpty();

            RuleFor(x => x.BirthShahrId)
               .NotEmpty();

            RuleFor(x => x.AddressShahrId)
               .NotEmpty();

            RuleFor(x => x.AddressOstanId)
               .NotEmpty();

            RuleFor(x => x.PersonalPicGUID)
               .MaximumLength(1000);

            RuleFor(x => x.KartMeliGUID)
               .MaximumLength(1000);

            RuleFor(x => x.ShenasnamehGUID)
               .MaximumLength(1000);
        }
    }
}
