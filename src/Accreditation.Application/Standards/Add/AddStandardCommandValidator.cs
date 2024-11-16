using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Standards.Add
{
    internal sealed class AddStandardCommandValidator : AbstractValidator<AddStandardCommand>
    {
        public AddStandardCommandValidator()
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
