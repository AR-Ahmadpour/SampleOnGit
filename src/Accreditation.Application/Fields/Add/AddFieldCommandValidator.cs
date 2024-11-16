using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Fields.Add
{
    internal sealed class AddFieldCommandValidator:AbstractValidator<AddFieldCommand>
    {
        public AddFieldCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.TitleCode)
               .NotEmpty()
               .MaximumLength(250);

            RuleFor(x => x.EtebarDorehGuid)
                .NotEmpty();

            RuleFor(x=>x.InstanceTypeIds)
                .NotEmpty();
        }
    }
}
