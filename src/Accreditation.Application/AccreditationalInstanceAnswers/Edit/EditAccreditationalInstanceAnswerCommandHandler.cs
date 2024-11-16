using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationalInstanceAnswers;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.AccreditationalInstanceAnswers.Edit
{
    internal class EditAccreditationalInstanceAnswerCommandHandler
        :ICommandHandler<EditAccreditationalInstanceAnswerCommand ,Guid>
    {
        private readonly IAccreditationInstanceAnswerRepository _accreditationInstanceAnswerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditAccreditationalInstanceAnswerCommandHandler(
            IAccreditationInstanceAnswerRepository accreditationInstanceAnswerRepository,
            IUnitOfWork unitOfWork)
        {
            _accreditationInstanceAnswerRepository = accreditationInstanceAnswerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(EditAccreditationalInstanceAnswerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if(command.result == -2)
                {
                    return Result.Failure<Guid>(AccreditationalInstanceAnswerError.ErroNoResponseAccreditationalInstanceAnswer);

                }
                if (command.result == -1 && (command.answerText ==null  ||command.answerText == ""  ))
                {
                  return  Result.Failure<Guid>(AccreditationalInstanceAnswerError.Erro_NA_AccreditationalInstanceAnswer);
                }
                var AccInsAnswer = await _accreditationInstanceAnswerRepository.FindAsyc(command.AccInsAnswerId, cancellationToken);
                if (AccInsAnswer == null)
                {
                    return Result.Failure<Guid>(AccreditationalInstanceAnswerError.ErroWhenAccreditationalInstanceAnswer);
                }
                AccInsAnswer.EditResult(command.answerText, command.result, command.UserId, DateTime.Now);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return command.AccInsAnswerId;

            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
                return Result.Failure<Guid>(AccreditationalInstanceAnswerError.ErroWhenAccreditationalInstanceAnswer);
            }
        }

    }
}
