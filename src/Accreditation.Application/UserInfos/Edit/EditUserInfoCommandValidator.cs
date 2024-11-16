using FluentValidation;
using System.Text.RegularExpressions;

namespace Accreditation.Application.UserInfos.Edit
{
    internal sealed class EditUserInfoCommandValidator:AbstractValidator<EditUserInfoCommand>
    {
        public EditUserInfoCommandValidator()
        {
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
               .NotEmpty();


            RuleFor(x => x.KartMeliGUID)
               .NotEmpty();


            RuleFor(x => x.ShenasnamehGUID)
               .NotEmpty();
        }
    }
}
