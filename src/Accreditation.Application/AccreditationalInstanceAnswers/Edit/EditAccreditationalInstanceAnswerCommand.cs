using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.AccreditationalInstanceAnswers.Edit
{
    public sealed record EditAccreditationalInstanceAnswerCommand
   (Guid AccInsAnswerId,
        string? answerText,
        decimal? result,
        Guid UserId
        )
        :ICommand<Guid>;
}
