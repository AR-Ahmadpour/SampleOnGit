using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using SharedKernel;

namespace Accreditation.Application.EvaluationArzyabs.Delete;

internal sealed class DeleteEvaluationArzyabCommandHandler
   : ICommandHandler<DeleteEvaluationArzyabCommand>
{
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEvaluationArzyabCommandHandler(IEvaluationArzyabRepository evaluationArzyabRepository,
                                               IUnitOfWork unitOfWork)
    {
        _evaluationArzyabRepository = evaluationArzyabRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(
        DeleteEvaluationArzyabCommand command,
        CancellationToken cancellationToken)
    {
        var evaluationArzyab = await _evaluationArzyabRepository.FindAsync(command.GUID, cancellationToken);
        if (evaluationArzyab == null)
        {
            return Result.Failure(EvaluationArzyabErrors.AccreditationNotFound);
        }

        _evaluationArzyabRepository.Delete(evaluationArzyab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}